namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PortController : ControllerBase
{
    private readonly IPortService _portService;

    public PortController(IPortService portService)
    {
        _portService = portService;
    }

    [HttpGet("getPorts")]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var result = await _portService.GetPorts();
        return Ok(result);
    }
}
