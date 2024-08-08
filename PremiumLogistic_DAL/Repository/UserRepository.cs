namespace PremiumLogistic_DAL.Repository;

public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    public UserRepository(PremiumLogisticDbContext context) : base(context) { }
}
