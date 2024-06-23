namespace PremiumLogistic_BAL.IServices;

public interface ILocalTransportationService
{
    Task<PagedResponseOffsetDto<LocalTransportationDto>> GetLocalPrices(int pageNumber, int pageSize);
}
