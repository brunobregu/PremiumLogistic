namespace PremiumLogistic_DAL.Repository;

public class OceanRepository : Repository<Ocean>, IOceanRepository
{
    public OceanRepository(PremiumLogisticDbContext context) : base(context) { }
}
