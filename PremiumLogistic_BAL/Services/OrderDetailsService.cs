namespace PremiumLogistic_BAL.Services;

public class OrderDetailsService : IOrderDetailsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    public OrderDetailsService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task AddOrderDetails(AddOrderDetailsDto orderDetailsDto, string email)
    {
        var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);
        orderDetails.CreatedBy = email;
        orderDetails.ToBePaid = orderDetails.PaymentStatus == "Paid" ? 0 : orderDetails.ToBePaid;
        _unitOfWork.OrderDetailsRepository.Insert(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task<List<OrderDetailsDto>> GetOrderDetails(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException($"Email {email} not found");
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
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Order with Id {id} not found");
        var result = _mapper.Map<OrderDetailsByIdDto>(orderDetails);
        return result;
    }

    public async Task UpdateOrderDetail(int id, AddOrderDetailsDto updateOrderDetail, string email)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Order with Id {id} not found");
        orderDetails.UpdatedOn = DateTime.Now;
        orderDetails.UpdatedBy = email;
        orderDetails.VIN = updateOrderDetail.VIN;
        orderDetails.Make = updateOrderDetail.Make;
        orderDetails.Model = updateOrderDetail.Model;
        orderDetails.Year = updateOrderDetail.Year;
        orderDetails.Lot = updateOrderDetail.Lot;
        orderDetails.DspOrderID = updateOrderDetail.DspOrderID;
        orderDetails.Port = updateOrderDetail.Port;
        orderDetails.InlandCargoloop = updateOrderDetail.InlandCargoloop;
        orderDetails.OcCargoloop = updateOrderDetail.OcCargoloop;
        orderDetails.Broker = updateOrderDetail.Broker;
        orderDetails.ClientTotal = updateOrderDetail.ClientTotal;
        orderDetails.InlandDspch = updateOrderDetail.InlandDspch;
        orderDetails.OcCost = updateOrderDetail.OcCost;
        orderDetails.TotalCost = updateOrderDetail.TotalCost;
        orderDetails.Profit = updateOrderDetail.Profit;
        orderDetails.Storage = updateOrderDetail.Storage;
        orderDetails.PaymentStatus = updateOrderDetail.PaymentStatus;
        orderDetails.PartlyPaid = updateOrderDetail.PartlyPaid;
        orderDetails.ToBePaid = updateOrderDetail.PaymentStatus == "Paid" ? 0 : updateOrderDetail.ToBePaid;

        _unitOfWork.OrderDetailsRepository.Update(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteOrderDetail(int id)
    {
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Order with Id {id} not found");
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
                            SumClientTotal = g.Sum(o => o.ClientTotal),
                            SumPartlyPaid = g.Sum(o => o.PartlyPaid),
                            SumToBePaid = g.Sum(o => o.ToBePaid)
                        })
                        .FirstOrDefault();
        return details;
    }

    public async Task<DetailsDto> MyDetails(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException($"Email {email} not found");
        var myDetails = await _unitOfWork.OrderDetailsRepository.GetManyAsync(x => x.UserId == user.Id);
        var details = myDetails.GroupBy(o => 1)
                        .Select(g => new DetailsDto
                        {
                            NumberOfOrders = g.Count(),
                            SumClientTotal = g.Sum(o => o.ClientTotal),
                            SumPartlyPaid = g.Sum(o => o.PartlyPaid),
                            SumToBePaid = g.Sum(o => o.ToBePaid)
                        })
                        .FirstOrDefault();
        return details;
    }
}
