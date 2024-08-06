namespace PremiumLogistic_BAL.IServices;

public interface IAuctionService
{
    Task<List<AuctionDto>> Get();
}
