using Microsoft.AspNetCore.Identity;

namespace PremiumLogistic_BAL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;
    public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailSender emailSender)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _emailSender = emailSender;
    }
    public async Task Register(RegisterDto registerDto)
    {
        ApplicationUser newUser = new ApplicationUser()
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.Now,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
        };

        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
            throw new BadRequestException($"User could not be created!{result.Errors?.FirstOrDefault()?.Description}");

        await _userManager.AddToRoleAsync(newUser, "Client");
    }

    public async Task<AuthResultDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is not null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return await GenerateJwtToken(user);
        else
            throw new BadRequestException("Please check your credentials");
    }

    public async Task RequestPasswordReset(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new BadRequestException("User not found.");
        user.TemporaryPassword = GenerateRandomPassword();
        user.TemporaryPasswordExpiration = DateTime.Now.AddMinutes(5);
        var updateResult = await _userManager.UpdateAsync(user);
        if (updateResult.Succeeded)
        {
            IEnumerable<string> emails = new string[] { email };
            Message message = new Message(emails, "Reset password", $"Your password is {user.TemporaryPassword}");
            await _emailSender.SendEmail(message);
        } 
    }

    public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email) ?? throw new BadRequestException("User not found.");
        if(user.TemporaryPassword != resetPasswordDto.TemporaryPassword)
            throw new BadRequestException("Temporary password is incorrect.");
        if(user.TemporaryPasswordExpiration <= DateTime.Now)
            throw new BadRequestException("Temporary password has expired.Please try again");

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
        var user = await _userManager.FindByEmailAsync(email) ?? throw new BadRequestException("User not found.");
        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        if(!result.Succeeded)
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);
    }
    public async Task<List<UsersOfRoleDto>> GetUsersOfRole(string role)
    {
        var existRole = await _roleManager.FindByNameAsync(role) ?? throw new NotFoundException("Role doesn't exist");
        var userRoles = await _userManager.GetUsersInRoleAsync(existRole.Name) ?? throw new NotFoundException($"There are not any users in role {role}");

        return _mapper.Map<List<UsersOfRoleDto>>(userRoles.ToList());
    }

    private async Task<AuthResultDto> GenerateJwtToken(ApplicationUser user)
    {
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        //Add User Roles
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

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
