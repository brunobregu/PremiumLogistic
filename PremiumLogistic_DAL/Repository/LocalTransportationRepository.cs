namespace PremiumLogistic_DAL.Repository;

public class LocalTransportationRepository : Repository<LocalTransportation>, ILocalTransportationRepository
{
    public LocalTransportationRepository(PremiumLogisticDbContext context) : base(context) {}


}
