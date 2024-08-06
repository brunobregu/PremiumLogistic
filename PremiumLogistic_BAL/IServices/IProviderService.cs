namespace PremiumLogistic_BAL.IServices;

public interface IProviderService
{
    Task<List<ProviderDto>> Get();
}
