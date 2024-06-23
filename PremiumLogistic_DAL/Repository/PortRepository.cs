namespace PremiumLogistic_DAL.Repository;

public class PortRepository : Repository<Port>, IPortRepository
{
    public PortRepository(PremiumLogisticDbContext context) : base(context) {}


}
