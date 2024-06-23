namespace PremiumLogistic_BAL.Services;

public class LocalTransportationService : ILocalTransportationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LocalTransportationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<PagedResponseOffsetDto<LocalTransportationDto>> GetLocalPrices(int pageNumber, int pageSize)
    {
        var localPrices = await _unitOfWork.LocalTransportationRepository.GetWithOffsetPagination(pageNumber, pageSize);
        var result = _mapper.Map<PagedResponseOffsetDto<LocalTransportationDto>>(localPrices);
        //await _unitOfWork.CommitAsync();

        return result;
    }
}
