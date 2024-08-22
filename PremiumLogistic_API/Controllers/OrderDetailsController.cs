namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class OrderDetailsController(IOrderDetailsService orderDetailsService, IStringLocalizer<Resource> localizer) : ControllerBase
{
    private readonly IOrderDetailsService _orderDetailsService = orderDetailsService;
    private readonly IStringLocalizer<Resource> _localizer = localizer;

    //api per shtimin e nje porosie
    [ServiceFilter(typeof(AuditLogAttribute))]
    [HttpPost("add")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> Add([FromBody] AddOrderDetailsDto addOrderDetailsDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        await _orderDetailsService.AddOrderDetails(addOrderDetailsDto, email);
        return Created(nameof(Add), _localizer["OrderAdded"].Value);
    }

    [HttpGet("myOrders")]
    [Authorize(Roles ="Client")]
    public async Task<IActionResult> MyOrders()
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
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

    [HttpGet("clientsWithOrders")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> ClientsWithOrders()
    {
        var result = await _orderDetailsService.ClientsWithOrders();
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
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        var result = await _orderDetailsService.MyDetails(email);
        return Ok(result);
    }

    //api per marrjen e detajeve qe do te shfaqen per update
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
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        var result = await _orderDetailsService.MyOrderDetailsById(id, email);
        return Ok(result);
    }

    //api per te pare detajet e porosise admini
    [HttpGet("adminOrderDetailsById")]
    [Authorize(Roles = "Admin, Account Manager")]
    public async Task<IActionResult> AdminOrderDetailsById([FromQuery] int id)
    {
        var result = await _orderDetailsService.GetOrderDetailsByIdForAdmin(id);
        return Ok(result);
    }

    //api per perditesimin e porosise
    [HttpPut("update")]
    [Authorize(Roles = "Admin, Account Manager")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateOrderDto update)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        await _orderDetailsService.UpdateOrderDetail(id, update, email);
        return Ok(string.Format(_localizer["OrderUpdated"].Value, id));
    }

    //api per ndryshimin e statusit te makines
    [HttpPut("updateCarStatus")]
    [Authorize(Roles = "Admin, Account Manager")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> UpdateCarStatus([FromQuery] int id, [FromForm] UpdateCarStatusDto statusDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        var result = await _orderDetailsService.UpdateCarStatus(id, statusDto, email);
        return Ok(result);
    }

    //api per fshirjen e nje porosie
    [HttpDelete("delete")]
    [Authorize(Roles = "Admin, Account Manager")]
    [ServiceFilter(typeof(AuditLogAttribute))]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new BadRequestException(_localizer["TryAgain"].Value);
        await _orderDetailsService.DeleteOrderDetail(id, email);
        return Ok(string.Format(_localizer["DeleteOrder"].Value, id));
    }

    //api per te shfaqur fotot
    [HttpGet("viewPhotos")]
    [Authorize]
    public async Task<IActionResult> ViewPhotos([FromQuery] int id)
    {
        var result = await _orderDetailsService.ViewPhotosOfOrder(id);
        return Ok(result);
    }

    //api per te shfaqur dokumentat
    [HttpGet("viewDocuments")]
    [Authorize]
    public async Task<IActionResult> ViewDocuments([FromQuery] int id)
    {
        var result = await _orderDetailsService.ViewDocumentsOfOrder(id);
        return Ok(result);
    }
}
