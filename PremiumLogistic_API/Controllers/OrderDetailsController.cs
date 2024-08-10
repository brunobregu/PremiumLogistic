namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class OrderDetailsController : ControllerBase
{
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
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
        await _orderDetailsService.AddOrderDetails(addOrderDetailsDto, email);
        return Created(nameof(Add), _localizer["OrderAdded"]);
    }

    [HttpGet("myOrders")]
    [Authorize(Roles ="Client")]
    public async Task<IActionResult> MyOrders()
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
        var result = await _orderDetailsService.MyOrders(email);
        return Ok(result);
    }

    [HttpGet("orders")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> Orders()
    {
        var result = await _orderDetailsService.GetAllOrderDetails();
        return Ok(result);
    }

    [HttpGet("ordersByClient")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> OrdersByClient([FromQuery] string userId)
    {
        var result = await _orderDetailsService.GetAllOrderDetailsByClient(userId);
        return Ok(result);
    }

    [HttpGet("details")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> Details()
    {
        var result = await _orderDetailsService.Details();
        return Ok(result);
    }

    [HttpGet("detailsByClient")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> DetailsByClient([FromQuery] string userId)
    {
        var result = await _orderDetailsService.DetailsByClient(userId);
        return Ok(result);
    }

    [HttpGet("myDetails")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> MyDetails()
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
        var result = await _orderDetailsService.MyDetails(email);
        return Ok(result);
    }

    [HttpGet("orderById")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var result = await _orderDetailsService.GetOrderDetailsById(id);
        return Ok(result);
    }

    [HttpGet("myOrderDetailsById")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> MyOrderDetailsById([FromQuery] int id)
    {
        var result = await _orderDetailsService.MyOrderDetailsById(id);
        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin, Account Manager")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] AddOrderDetailsDto update)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
        await _orderDetailsService.UpdateOrderDetail(id, update, email);
        return Ok($"Order with id {id} is updated successully");
    }

    [HttpPut("updateCarStatus")]
    [Authorize(Roles = "Admin, Account Manager")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> UpdateCarStatus([FromQuery] int id, [FromForm] UpdateCarStatusDto statusDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
        await _orderDetailsService.UpdateCarStatus(id, statusDto, email);
        return Ok(string.Format(_localizer["CarStatusUpdated"], statusDto.Status, id));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin, Account Manager")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"]);
        await _orderDetailsService.DeleteOrderDetail(id, email);
        return Ok(string.Format(_localizer["DeleteOrder"], id));
    }

    [HttpGet("viewPhotosOfOrder")]
    [Authorize(Roles = "Client, Admin, Account Manager")]
    public async Task<IActionResult> ViewPhotosOfOrder([FromQuery] int id)
    {
        //var result = _orderDetailsService.
        return Ok();
    }
}
