namespace PremiumLogistic_BAL.IServices;

public interface ITransportationService
{
    Task<TransportationDto> GetPrice(string zip, string terminal);
}
