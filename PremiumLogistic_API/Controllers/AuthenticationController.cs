namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class AuthenticationController(IUserService userService, IStringLocalizer<Resource> localizer) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IStringLocalizer<Resource> _localizer = localizer;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        await _userService.Register(registerDto);
        return Created(nameof(Register), string.Format(_localizer["UserCreated"].Value, registerDto.Email));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _userService.Login(loginDto);
        return Ok(result);
    }

    [ServiceFilter(typeof(AuditLogAttribute))]
    [HttpPost("addUser")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddUser([FromBody] CreateUserDto createUserDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        var result = await _userService.AddUser(createUserDto, email);
        return Created(nameof(AddUser), result);
    }

    [HttpPost("requestResetPassword")]
    public async Task<IActionResult> RequestResetPassword([FromBody] RequestResetPasswordDto requestResetPassword)
    {
        var result = await _userService.RequestPasswordReset(requestResetPassword.Email);
        return Ok(result);
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
    {
        await _userService.ResetPassword(request);
        return Ok();
    }

    [HttpPost("changePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        await _userService.ChangePassword(request, email);
        return Ok();
    }

    [HttpGet("getUsersOfRole")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get([FromQuery] string role)
    {
        var result = await _userService.GetUsersOfRole(role);
        return Ok(result);
    }

    [HttpGet("getRoles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _userService.GetRoles();
        return Ok(result);
    }

    [HttpPost("addRole")]
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> AddRole(AddRoleDto addRoleDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        await _userService.AddRole(addRoleDto, email);
        return Created(nameof(AddRole), string.Format(_localizer["RoleCreated"].Value, addRoleDto.Name));
    }

}
