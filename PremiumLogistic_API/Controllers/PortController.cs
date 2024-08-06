namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class PortController : ControllerBase
{
    private readonly IPortService _portService;
    public PortController(IPortService portService)
    {
        _portService = portService;
    }

    [Authorize(Roles = "Admin, Account Manager")]
    [HttpGet("ports")]
    public async Task<IActionResult> Get()
    {
        var result = await _portService.Get();
        return Ok(result);
    }
}
