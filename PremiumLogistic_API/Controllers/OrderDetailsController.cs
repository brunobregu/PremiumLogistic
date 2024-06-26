namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class OrderDetailsController : ControllerBase
{
    private const string emailType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    private readonly IOrderDetailsService _orderDetailsService;
    public OrderDetailsController(IOrderDetailsService orderDetailsService)
    {
        _orderDetailsService = orderDetailsService;
    }

    [HttpPost("addOrderDetails")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] AddOrderDetailsDto addOrderDetailsDto)
    {
        await _orderDetailsService.AddOrderDetails(addOrderDetailsDto);
        return Created(nameof(Add), "Order details added successfully");
    }

    [HttpGet("myOrders")]
    [Authorize(Roles ="Client")]
    public async Task<IActionResult> MyOrders()
    {
        var email = User.Claims.FirstOrDefault(x => x.Type == emailType)?.Value;
        var result = await _orderDetailsService.GetOrderDetails(email);
        return Ok(result);
    }

    [HttpGet("orders")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Orders()
    {
        var result = await _orderDetailsService.GetAllOrderDetails();
        return Ok(result);
    }
}
