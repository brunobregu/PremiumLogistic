namespace PremiumLogistic_DAL.Repository;

public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
{
    public OrderDetailsRepository(PremiumLogisticDbContext context) : base(context) { }
}
