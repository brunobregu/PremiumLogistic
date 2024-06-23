namespace PremiumLogistic_BAL.IServices;

public interface IPortService
{
    Task<List<PortDto>> GetPorts();

}
