namespace PremiumLogistic_BAL.IServices;

public interface IOrderDetailsService
{
    Task AddOrderDetails(AddOrderDetailsDto orderDetailsDto, string email);
    Task<List<OrderDetailsDto>> MyOrders(string username);
    Task<List<AllOrderDetailsDto>> GetAllOrderDetails();
    Task<OrderDetailsByIdDto> GetOrderDetailsById(int id);
    Task<AdminOrderDetailsByIdDto> GetOrderDetailsByIdForAdmin(int id);
    Task<DetailsDto> Details();
    Task<DetailsDto> MyDetails(string email);
    Task UpdateOrderDetail(int id, UpdateOrderDto updateOrderDetail, string email);
    Task DeleteOrderDetail(int id, string email);
    Task<List<AllOrderDetailsDto>> GetAllOrderDetailsByClient(string userId);
    Task<DetailsDto> DetailsByClient(string userId);
    Task UpdateCarStatus(int id, UpdateCarStatusDto updateOrderDetail, string email);
    Task<MyOrderDetailsByIdDto> MyOrderDetailsById(int id);
}
