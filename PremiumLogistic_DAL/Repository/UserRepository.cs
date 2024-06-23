namespace PremiumLogistic_DAL.Repository;

public class UserRepository : Repository<RefreshToken>, IUserRepository
{
    public UserRepository(PremiumLogisticDbContext context) : base(context) { }
}
