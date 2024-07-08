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
    public async Task AddOrderDetails(AddOrderDetailsDto orderDetailsDto)
    {
        var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);
        _unitOfWork.OrderDetailsRepository.Insert(orderDetails);
        await _unitOfWork.CommitAsync();
    }

    public async Task<List<OrderDetailsDto>> GetOrderDetails(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new Exception($"Email {email} not found");
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
        var user = await _userManager.FindByEmailAsync(email) ?? throw new Exception($"Email {email} not found");
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
