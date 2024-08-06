namespace PremiumLogistic_BAL.IServices;

public interface IOrderDetailsService
{
    Task AddOrderDetails(AddOrderDetailsDto orderDetailsDto, string email);
    Task<List<OrderDetailsDto>> MyOrders(string username);
    Task<List<AllOrderDetailsDto>> GetAllOrderDetails();
    Task<OrderDetailsByIdDto> GetOrderDetailsById(int id);
    Task<DetailsDto> Details();
    Task<DetailsDto> MyDetails(string email);
    Task UpdateOrderDetail(int id, AddOrderDetailsDto updateOrderDetail, string email);
    Task DeleteOrderDetail(int id);
}
