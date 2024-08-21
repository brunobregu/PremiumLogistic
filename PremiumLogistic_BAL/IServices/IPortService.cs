namespace PremiumLogistic_BAL.IServices;

public interface IPortService
{
    Task<List<PortDto>> Get();
    Task Add(AddPortDto addPort);
    Task Delete(int id);
}
