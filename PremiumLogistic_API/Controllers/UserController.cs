namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]
public class UserController(IUserService userService, IStringLocalizer<Resource> localizer) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IStringLocalizer<Resource> _localizer = localizer;

    [HttpGet("activeUsers")]
    public async Task<IActionResult> ActiveUsers()
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        var result = await _userService.ActiveUsers(email);
        return Ok(result);
    }

    [HttpGet("nonActiveUsers")]
    public async Task<IActionResult> NonActiveUsers()
    {
        var result = await _userService.NonActiveUsers();
        return Ok(result);
    }

    [HttpDelete("deleteUser")]
    public async Task<IActionResult> DeleteUser([FromQuery] string userId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        await _userService.DeleteUser(userId, email);
        return Ok("User deleted successfully!");
    }

    [HttpPost("activateUser")]
    public async Task<IActionResult> ActivateUser([FromQuery] string userId, [FromQuery] string role)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        await _userService.ActivateUser(userId, email, role);
        return Ok("User activated successfully!");
    }

    [HttpDelete("deleteRole")]
    public async Task<IActionResult> DeleteRole([FromQuery] string role)
    {
        await _userService.DeleteRole(role);
        return Ok("Role deleted successfully!");
    }
}
