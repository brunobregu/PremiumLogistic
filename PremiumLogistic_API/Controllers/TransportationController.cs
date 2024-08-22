namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class TransportationController(ITransportationService transportationService) : ControllerBase
{
    private readonly ITransportationService _transportationService = transportationService;

    [HttpGet("price")]
    public async Task<IActionResult> Get([FromQuery] string zip, [FromQuery] string terminal)
    {
        var result = await _transportationService.GetPrice(zip,terminal);
        return Ok(result);
    }
}
