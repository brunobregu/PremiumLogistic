namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin, Account Manager")]
public class AuctionController(IAuctionService auctionService) : ControllerBase
{
    private readonly IAuctionService _auctionService = auctionService;

    [HttpGet("auctions")]
    public async Task<IActionResult> Get()
    {
        var result = await _auctionService.Get();
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddAuctionDto addAuction)
    {
        await _auctionService.Add(addAuction);
        return Ok("Auction added successfully");
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        await _auctionService.Delete(id);
        return Ok("Auction deleted successfully");
    }
}
