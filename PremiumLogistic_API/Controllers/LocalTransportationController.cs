namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class LocalTransportationController : ControllerBase
{
    private readonly ILocalTransportationService _localTransportationPriceService;
    public LocalTransportationController(ILocalTransportationService localTransportationPriceService)
    {
        _localTransportationPriceService = localTransportationPriceService;
    }

    [HttpGet("getLocalPrice")]
    public async Task<IActionResult> Get(int? pageNumber, int? pageSize)
    {
        var result = await _localTransportationPriceService.GetLocalPrices(pageNumber ?? 1, pageSize ?? 10);
        return Ok(result);
    }
}
