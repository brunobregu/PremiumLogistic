namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class OrderDetailsController : ControllerBase
{
    private readonly IOrderDetailsService _orderDetailsService;
    public OrderDetailsController(IOrderDetailsService orderDetailsService)
    {
        _orderDetailsService = orderDetailsService;
    }

    [HttpPost("addOrderDetails")]
    public async Task<IActionResult> Add([FromBody] AddOrderDetailsDto addOrderDetailsDto)
    {
        await _orderDetailsService.AddOrderDetails(addOrderDetailsDto);
        return Created(nameof(Add), "Order details added successfully");
    }

    [HttpGet("getOrderDetails")]
    [Authorize(Roles ="Client")]
    public async Task<IActionResult> Get()
    {
        var userid = User.Identity.Name;
        var result = await _orderDetailsService.GetOrderDetails(userid);
        return Ok(result);
    }
}
