using PremiumLogistic_BAL.Dtos.Port;

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

    [Authorize(Roles = "Admin, Account Manager")]
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddPortDto addPort)
    {
        await _portService.Add(addPort);
        return Ok("Port added successfully");
    }

    [Authorize(Roles = "Admin, Account Manager")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        await _portService.Delete(id);
        return Ok("Port deleted successfully");
    }
}
