namespace PremiumLogistic_DAL.Repository;

public class ProviderRepository : Repository<Provider>, IProviderRepository
{
    public ProviderRepository(PremiumLogisticDbContext context) : base(context) { }
}
