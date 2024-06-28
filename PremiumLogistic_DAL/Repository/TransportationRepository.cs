namespace PremiumLogistic_DAL.Repository;

public class TransportationRepository : Repository<Transportation>, ITransportationRepository
{
    public TransportationRepository(PremiumLogisticDbContext context) : base(context) {}


}
