namespace PremiumLogistic_BAL.Services;

public class OrderDetailsService : IOrderDetailsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<Resource> _localizer;
    public OrderDetailsService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IStringLocalizer<Resource> localizer)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _localizer = localizer;
    }
    public async Task AddOrderDetails(AddOrderDetailsDto orderDetailsDto, string email)
    {
        var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);
        orderDetails.CreatedBy = email;
        orderDetails.CarStatus = "Dispatch";
        //orderDetails.ToBePaid = orderDetails.PaymentStatus == PaymentStatus.PartlyPaid ? orderDetails.ToBePaid : 0;
        _unitOfWork.OrderDetailsRepository.Insert(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task<List<OrderDetailsDto>> GetOrderDetails(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException(string.Format(_localizer["EmailNotFound"], email));
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetManyAsync(x => x.UserId == user.Id);
        var result = _mapper.Map<List<OrderDetailsDto>>(orderDetails);
        return result;
    }

    public async Task<List<AllOrderDetailsDto>> GetAllOrderDetails()
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.IncludeAsync(c => c.User);
        var result = _mapper.Map<List<AllOrderDetailsDto>>(orderDetails);
        return result;
    }

    public async Task<OrderDetailsByIdDto> GetOrderDetailsById(int id)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.IncludeAsync(c => c.User);
        var ordersById = orderDetails.Where(x => x.Id== id).FirstOrDefault() ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"], id));
        var result = _mapper.Map<OrderDetailsByIdDto>(ordersById);
        return result;
    }

    public async Task UpdateOrderDetail(int id, AddOrderDetailsDto updateOrderDetail, string email)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"], id));
        orderDetails.UpdatedOn = DateTime.Now;
        orderDetails.UpdatedBy = email;
        orderDetails.VIN = updateOrderDetail.VIN;
        orderDetails.Make = updateOrderDetail.Make;
        orderDetails.Model = updateOrderDetail.Model;
        orderDetails.Year = updateOrderDetail.Year;
        orderDetails.Lot = updateOrderDetail.Lot;
        //orderDetails.DspOrderID = updateOrderDetail.DspOrderID;
        orderDetails.Port = updateOrderDetail.Port;
        orderDetails.InlandPrice = updateOrderDetail.InlandPrice;
        orderDetails.OceanPrice = updateOrderDetail.OceanPrice;
        orderDetails.Broker = updateOrderDetail.Broker;
        orderDetails.ClientTotal = updateOrderDetail.ClientTotal;
        orderDetails.InlandCost = updateOrderDetail.InlandCost;
        orderDetails.OceanCost = updateOrderDetail.OceanCost;
        orderDetails.TotalCost = updateOrderDetail.TotalCost;
        orderDetails.Profit = updateOrderDetail.Profit;
        //orderDetails.Storage = updateOrderDetail.Storage;
        //orderDetails.PaymentStatus = updateOrderDetail.PaymentStatus;
        orderDetails.PartlyPaid = updateOrderDetail.PartlyPaid;
        orderDetails.ToBePaid = updateOrderDetail.PaymentStatus == "Partly Paid" ? updateOrderDetail.ToBePaid : 0;

        _unitOfWork.OrderDetailsRepository.Update(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteOrderDetail(int id)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(_localizer["OrderNotFound"], id));
        orderDetails.Invalidated = true;
        
        _unitOfWork.OrderDetailsRepository.Update(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task<DetailsDto> Details()
    {
        var alldetails = await _unitOfWork.OrderDetailsRepository.GetAllAsync();
        var details = alldetails.GroupBy(o => 1)
                        .Select(g => new DetailsDto
                        {
                            NumberOfOrders = g.Count(),
                            ClientTotal = g.Sum(o => o.ClientTotal),
                            ToBePaid = g.Sum(o => o.ToBePaid)
                        })
                        .FirstOrDefault();
        return details;
    }

    public async Task<DetailsDto> MyDetails(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException(string.Format(_localizer["EmailNotFound"], email));
        var myDetails = await _unitOfWork.OrderDetailsRepository.GetManyAsync(x => x.UserId == user.Id);
        var details = myDetails.GroupBy(o => 1)
                        .Select(g => new DetailsDto
                        {
                            NumberOfOrders = g.Count(),
                            ClientTotal = g.Sum(o => o.ClientTotal),
                            ToBePaid = g.Sum(o => o.ToBePaid)
                        })
                        .FirstOrDefault();
        return details;
    }
}
