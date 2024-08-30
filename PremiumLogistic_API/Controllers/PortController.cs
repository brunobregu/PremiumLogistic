namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin, Account Manager")]
public class PortController(IPortService portService, IStringLocalizer<Resource> localizer) : ControllerBase
{
    private readonly IPortService _portService = portService;
    private readonly IStringLocalizer<Resource> _localizer = localizer;

    [HttpGet("ports")]
    public async Task<IActionResult> Get()
    {
        var result = await _portService.Get();
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddPortDto addPort)
    {
        await _portService.Add(addPort);
        return Ok(_localizer["PortAdded"].Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        await _portService.Delete(id);
        return Ok(_localizer["PortDeleted"].Value);
    }
}
