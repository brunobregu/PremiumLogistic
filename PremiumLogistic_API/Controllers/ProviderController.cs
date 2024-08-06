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
}
