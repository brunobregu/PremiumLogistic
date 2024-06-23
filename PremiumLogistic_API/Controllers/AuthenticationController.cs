namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        await _userService.Register(registerDto);
        return Created(nameof(Register), $"User {registerDto.Email} created");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _userService.Login(loginDto);
        return Ok(result);
    }

    [HttpGet("getUsersOfRole")]
    public async Task<IActionResult> Get([FromQuery] string role)
    {
        var result = await _userService.GetUsersOfRole(role);
        return Ok(result);
    }
}
