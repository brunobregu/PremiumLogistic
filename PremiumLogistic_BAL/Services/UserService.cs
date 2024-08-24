namespace PremiumLogistic_BAL.Services;

public class UserService
(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration,
    IEmailSender emailSender,
    IStringLocalizer<Resource> localizer
) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IStringLocalizer<Resource> _localizer = localizer;

    public async Task<string> AddUser(CreateUserDto createUserDto, string email)
    {       
        var newUser = _mapper.Map<ApplicationUser>(createUserDto);
        newUser.UserName = createUserDto.Email;
        newUser.CreatedBy = email;

        var resultUser = await _userManager.CreateAsync(newUser, createUserDto.Password);
        if (!resultUser.Succeeded)
            throw new BadRequestException(resultUser.Errors?.FirstOrDefault()?.Description ?? "Please try again");

        var resultRole = await _userManager.AddToRoleAsync(newUser, createUserDto.RoleName);
        if (!resultRole.Succeeded)
            throw new BadRequestException(resultRole.Errors?.FirstOrDefault()?.Description ?? "Please try again");

        try
        {
            IEnumerable<string> emails = new string[] { createUserDto.Email };
            Message message = new Message(emails, "Kredencialet tuaja - Your credentials", string.Format(_configuration["GeneralConfigs:AddUser"], createUserDto.Email, createUserDto.Password));
            await _emailSender.SendEmail(message);
        }
        catch
        {
            return "User created, but email with credentials not send!";
        }

        return string.Format(_localizer["UserCreated"].Value, createUserDto.Email);
    }
    public async Task Register(RegisterDto registerDto)
    {
        var newUser = _mapper.Map<ApplicationUser>(registerDto);
        newUser.UserName = registerDto.Email;
        newUser.CreatedBy = "register";
        var resultUser = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!resultUser.Succeeded)
            throw new BadRequestException(resultUser.Errors?.FirstOrDefault()?.Description ?? "Please try again");

        var resultRole = await _userManager.AddToRoleAsync(newUser, "Client");
        if(!resultRole.Succeeded)
            throw new BadRequestException(resultUser.Errors?.FirstOrDefault()?.Description ?? "Please try again");
    }

    public async Task<AuthResultDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is not null && !user.Invalidated && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return await GenerateJwtToken(user);
        else
            throw new BadRequestException(_localizer["CheckCredentials"].Value);
    }

    public async Task<string> RequestPasswordReset(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        if (user.Invalidated)
            throw new NotFoundException("Your account is not active!");

        user.TemporaryPassword = GenerateRandomPassword();
        user.TemporaryPasswordExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["GeneralConfigs:PasswordExpire"]));
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            throw new BadRequestException(updateResult.Errors?.FirstOrDefault()?.Description ?? "Please try again");

        try
        {
            IEnumerable<string> emails = new string[] { email };
            Message message = new Message(emails, "Reset password", string.Format(_localizer["TempPass"].Value, user.TemporaryPassword));
            await _emailSender.SendEmail(message);
        }
        catch
        {
            return "User created, but email with credentials not send!";
        }

        return string.Format(_localizer["TempPassSend"].Value, email);
    }

    public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        
        if (user.Invalidated)
            throw new NotFoundException("Your account is not active!");
        if (user.TemporaryPassword != resetPasswordDto.TemporaryPassword)
            throw new BadRequestException(_localizer["TempPassIncorrect"].Value);
        if(user.TemporaryPasswordExpiration <= DateTime.Now)
            throw new BadRequestException(_localizer["TempPassExpire"].Value);

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDto.NewPassword);
        if (!resetResult.Succeeded)
            throw new BadRequestException(resetResult.Errors?.FirstOrDefault()?.Description ?? "Please try again");

        user.TemporaryPassword = null;
        user.TemporaryPasswordExpiration = null;
        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new BadRequestException(resetResult.Errors?.FirstOrDefault()?.Description ?? "Please try again");
    }

    public async Task ChangePassword(ChangePasswordDto changePasswordDto, string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        if(!result.Succeeded)
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description ?? "Please try again");
    }
    public async Task<List<UsersOfRoleDto>> GetUsersOfRole(string role)
    {
        var existRole = await _roleManager.FindByNameAsync(role) ?? throw new NotFoundException(_localizer["RoleNotExist"].Value);
        var userRoles = await _userManager.GetUsersInRoleAsync(existRole.Name) ?? throw new NotFoundException(string.Format(_localizer["NotUserInRole"].Value, role));

        return _mapper.Map<List<UsersOfRoleDto>>(userRoles.ToList());
    }

    public async Task<List<RolesDto>> GetRoles()
    {
        var roles = _roleManager.Roles;
        return _mapper.Map<List<RolesDto>>(await roles.ToListAsync());
    }

    public async Task AddRole(AddRoleDto addRoleDto, string email)
    {
        var addRole = _mapper.Map<ApplicationRole>(addRoleDto);
        addRole.CreatedBy = email;
        var result = await _roleManager.CreateAsync(addRole);
        if(!result.Succeeded)
            throw new BadRequestException(result.Errors?.FirstOrDefault()?.Description);
    }

    public async Task<List<UserDto>> ActiveUsers(string email)
    {
        var users = await _userManager.Users
                             .Where(user => !user.Invalidated && user.UserName != email)
                             .ToListAsync();
        List<UserDto> result = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Role = roles
            }) ;
        }
            return result;
    }

    public async Task<List<UserDto>> NonActiveUsers()
    {
        var users = await _userManager.Users
                             .Where(user => user.Invalidated)
                             .ToListAsync();
        List<UserDto> result = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Role = roles
            });
        }
        return result;
    }

    public async Task DeleteUser(string id, string email)
    {
        var user = await _userManager.FindByIdAsync(id) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        var roles = await _userManager.GetRolesAsync(user);
        user.Invalidated = true;
        user.UpdatedOn = DateTime.Now;
        user.UpdatedBy = email;
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CommitAsync();
        var result = await _userManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
        if (!result.Succeeded)
            throw new BadRequestException("Something wrong to delete user!");
    }

    public async Task ActivateUser(string id, string email, string role)
    {
        var user = await _userManager.FindByIdAsync(id) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        user.Invalidated = false;
        user.UpdatedOn = DateTime.Now;
        user.UpdatedBy = email;
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CommitAsync();
        var result = await _userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
            throw new BadRequestException("Something wrong to activate user!");
    }

    public async Task DeleteRole(string role)
    {
        var existRole = await _roleManager.FindByNameAsync(role) ?? throw new NotFoundException(_localizer["RoleNotExist"].Value);
        var userRoles = await _userManager.GetUsersInRoleAsync(existRole.Name) ?? throw new NotFoundException(string.Format(_localizer["NotUserInRole"].Value, role));
        if (userRoles.Count != 0)
            throw new BadRequestException("Please delete all users, before deleting role!");
        var result = await _roleManager.DeleteAsync(existRole);
        if (!result.Succeeded)
            throw new BadRequestException(result.Errors?.FirstOrDefault()?.Description ?? "Please try again!");
    }

    public async Task<PersonalDataDto> PersonalData(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        var result = _mapper.Map<PersonalDataDto>(user);
        return result;
    }

    private async Task<AuthResultDto> GenerateJwtToken(ApplicationUser user)
    {
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        //Add User Roles
        var userRoles = await _userManager.GetRolesAsync(user);
        authClaims.Add(new Claim(ClaimTypes.Role, userRoles.FirstOrDefault() ?? ""));

        var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["JWT:TokenExpirationTime"])),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new AuthResultDto()
        {
            Token = jwtToken,
            Role = userRoles.FirstOrDefault() ?? ""
        };

        return response;
    }

    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();

        string part1 = new string(Enumerable.Repeat(chars, 3)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        string part2 = new string(Enumerable.Repeat(chars, 3)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return $"{part1}-{part2}";
    }
}
