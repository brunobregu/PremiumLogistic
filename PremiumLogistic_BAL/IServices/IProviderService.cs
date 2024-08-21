namespace PremiumLogistic_BAL.IServices;

public interface IProviderService
{
    Task<List<ProviderDto>> Get();
    Task Add(AddProviderDto addProvider);
    Task Delete(int id);
}
