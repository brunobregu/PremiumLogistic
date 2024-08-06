namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService _auctionService;
    public AuctionController(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    [Authorize(Roles = "Admin, Account Manager")]
    [HttpGet("auctions")]
    public async Task<IActionResult> Get()
    {
        var result = await _auctionService.Get();
        return Ok(result);
    }
}
