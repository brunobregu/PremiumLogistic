namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class OrderDetailsController : ControllerBase
{
    private const string emailType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    private readonly IOrderDetailsService _orderDetailsService;
    private readonly IStringLocalizer<Resource> _localizer;
    public OrderDetailsController(IOrderDetailsService orderDetailsService, IStringLocalizer<Resource> localizer)
    {
        _orderDetailsService = orderDetailsService;
        _localizer = localizer;

    }

    [ServiceFilter(typeof(AuditLogAttribute))]
    [HttpPost("add")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> Add([FromBody] AddOrderDetailsDto addOrderDetailsDto)
    {
        var email = User.Claims.FirstOrDefault(x => x.Type == emailType)?.Value;
        await _orderDetailsService.AddOrderDetails(addOrderDetailsDto, email);
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

    [HttpGet("details")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details()
    {
        var result = await _orderDetailsService.Details();
        return Ok(result);
    }

    [HttpGet("myDetails")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> MyDetails()
    {
        var email = User.Claims.FirstOrDefault(x => x.Type == emailType)?.Value;
        var result = await _orderDetailsService.MyDetails(email);
        return Ok(result);
    }

    [HttpGet("orderById")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _orderDetailsService.GetOrderDetailsById(id);
        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin, Account Manager")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> Update(int id, AddOrderDetailsDto update)
    {
        var email = User.Claims.FirstOrDefault(x => x.Type == emailType)?.Value;
        await _orderDetailsService.UpdateOrderDetail(id, update, email);
        return Ok($"Order with id {id} is updated successully");
    }

    //[HttpPut("updateStatus")]
    //[Authorize(Roles = "Admin, Account Manager")]
    //[ServiceFilter(typeof(AuditLogAttribute))]
    //public async Task<IActionResult> UpdateStatus(int id, )

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin, Account Manager")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderDetailsService.DeleteOrderDetail(id);
        return Ok($"Order with id {id} is deleted successully");
    }
}
