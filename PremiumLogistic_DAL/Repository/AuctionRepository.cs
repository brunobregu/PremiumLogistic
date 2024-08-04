namespace PremiumLogistic_DAL.Repository;

public class AuctionRepository : Repository<Auction>, IAuctionRepository
{
    public AuctionRepository(PremiumLogisticDbContext context) : base(context) { }
}
