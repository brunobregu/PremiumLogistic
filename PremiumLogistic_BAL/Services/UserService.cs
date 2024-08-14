namespace PremiumLogistic_BAL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;
    private readonly IStringLocalizer<Resource> _localizer;
    public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailSender emailSender, IStringLocalizer<Resource> localizer)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _emailSender = emailSender;
        _localizer = localizer;
    }

    public async Task AddUser(CreateUserDto createUserDto, string email)
    {       
        var newUser = _mapper.Map<ApplicationUser>(createUserDto);
        newUser.UserName = createUserDto.Email;
        newUser.CreatedBy = email;
        var resultUser = await _userManager.CreateAsync(newUser, createUserDto.Password);
        if (!resultUser.Succeeded)
            throw new BadRequestException(string.Format(_localizer["UserNotCreated"].Value, resultUser.Errors?.FirstOrDefault()?.Description));

        var resultRole = await _userManager.AddToRoleAsync(newUser, createUserDto.RoleName);
        if (!resultRole.Succeeded)
            throw new BadRequestException(string.Format(_localizer["UserNotCreated"].Value, resultRole.Errors?.FirstOrDefault()?.Description));
        
        IEnumerable<string> emails = new string[] { createUserDto.Email };
        Message message = new Message(emails, "Kredencialet tuaja - Your credentials", string.Format(_configuration["GeneralConfigs:AddUser"], createUserDto.Email, createUserDto.Password));
        await _emailSender.SendEmail(message);
    }
    public async Task Register(RegisterDto registerDto)
    {
        var newUser = _mapper.Map<ApplicationUser>(registerDto);
        newUser.UserName = registerDto.Email;
        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
            throw new BadRequestException(string.Format(_localizer["UserNotCreated"].Value, result.Errors?.FirstOrDefault()?.Description));

        await _userManager.AddToRoleAsync(newUser, "Client");
    }

    public async Task<AuthResultDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is not null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return await GenerateJwtToken(user);
        else
            throw new BadRequestException(_localizer["CheckCredentials"].Value);
    }

    public async Task RequestPasswordReset(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        user.TemporaryPassword = GenerateRandomPassword();
        user.TemporaryPasswordExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["GeneralConfigs:PasswordExpire"]));
        var updateResult = await _userManager.UpdateAsync(user);
        if (updateResult.Succeeded)
        {
            IEnumerable<string> emails = new string[] { email };
            Message message = new Message(emails, "Reset password", string.Format(_localizer["TempPass"].Value, user.TemporaryPassword));
            await _emailSender.SendEmail(message);
        } 
    }

    public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        if(user.TemporaryPassword != resetPasswordDto.TemporaryPassword)
            throw new BadRequestException(_localizer["TempPassIncorrect"].Value);
        if(user.TemporaryPasswordExpiration <= DateTime.Now)
            throw new BadRequestException(_localizer["TempPassExpire"].Value);

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDto.NewPassword);
        if (!resetResult.Succeeded)
            throw new BadRequestException(resetResult.Errors.FirstOrDefault()?.Description);

        user.TemporaryPassword = null;
        user.TemporaryPasswordExpiration = null;
        await _userManager.UpdateAsync(user);
    }

    public async Task ChangePassword(ChangePasswordDto changePasswordDto, string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new BadRequestException(_localizer["UserNotFound"].Value);
        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        if(!result.Succeeded)
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);
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
        var existRole = await _roleManager.FindByNameAsync(addRoleDto.Name);
        if (existRole is not null)
            throw new BadRequestException(string.Format(_localizer["RoleExist"].Value, addRoleDto.Name));

        var addRole = _mapper.Map<ApplicationRole>(addRoleDto);
        addRole.CreatedBy = email;
        await _roleManager.CreateAsync(addRole);
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
