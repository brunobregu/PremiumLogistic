namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin, Account Manager")]
public class ProviderController(IProviderService providerService, IStringLocalizer<Resource> localizer) : ControllerBase
{
    private readonly IProviderService _providerService = providerService;
    private readonly IStringLocalizer<Resource> _localizer = localizer;

    [HttpGet("providers")]
    public async Task<IActionResult> Get()
    {
        var result = await _providerService.Get();
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddProviderDto addPort)
    {
        await _providerService.Add(addPort);
        return Ok(_localizer["ProviderAdded"].Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        await _providerService.Delete(id);
        return Ok(_localizer["ProviderDeleted"].Value);
    }
}
