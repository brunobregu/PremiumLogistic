namespace PremiumLogistic_API.Controllers;
[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;
    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [Authorize(Roles = "Admin, Account Manager")]
    [HttpGet("providers")]
    public async Task<IActionResult> Get()
    {
        var result = await _providerService.Get();
        return Ok(result);
    }

    [Authorize(Roles = "Admin, Account Manager")]
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddProviderDto addPort)
    {
        await _providerService.Add(addPort);
        return Ok("Provider added successfully");
    }

    [Authorize(Roles = "Admin, Account Manager")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        await _providerService.Delete(id);
        return Ok("Provider deleted successfully");
    }
}
