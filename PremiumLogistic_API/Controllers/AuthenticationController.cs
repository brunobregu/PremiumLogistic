using PremiumLogistic_BAL.Dtos.Authentication;

namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IStringLocalizer<Resource> _localizer;
    public AuthenticationController(IUserService userService, IStringLocalizer<Resource> localizer)
    {
        _userService = userService;
        _localizer = localizer;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        await _userService.Register(registerDto);
        return Created(nameof(Register), string.Format(_localizer["UserCreated"], registerDto.Email));
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
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
        await _userService.AddUser(createUserDto, email);
        return Created(nameof(AddUser), string.Format(_localizer["UserCreated"], createUserDto.Email));
    }

    [HttpPost("requestResetPassword")]
    public async Task<IActionResult> RequestResetPassword([FromBody] string email)
    {
        await _userService.RequestPasswordReset(email);
        return Ok(string.Format(_localizer["TempPassSend"], email));
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
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
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

    [HttpPost("getRoles")]
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
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
        await _userService.AddRole(addRoleDto, email);
        return Created(nameof(AddRole), string.Format(_localizer["RoleCreated"]));
    }
}
