namespace PremiumLogistic_BAL.IServices;

public interface IOrderDetailsService
{
    Task AddOrderDetails(AddOrderDetailsDto orderDetailsDto);
    Task<List<OrderDetailsDto>> GetOrderDetails(string username);
    Task<List<AllOrderDetailsDto>> GetAllOrderDetails();
    Task<DetailsDto> Details();
    Task<DetailsDto> MyDetails(string email);
}
