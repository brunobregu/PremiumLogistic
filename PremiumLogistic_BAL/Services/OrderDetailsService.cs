

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

    public async Task<List<OrderDetailsDto>> GetOrderDetails(string username)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new Exception($"Username {username} not found");
        var orderDetails = await _unitOfWork.OrderDetailsRepository.GetManyAsync(x => x.UserId == user.Id);
        var result = _mapper.Map<List<OrderDetailsDto>>(orderDetails);
        //await _unitOfWork.CommitAsync();
        return result;
    }
}
