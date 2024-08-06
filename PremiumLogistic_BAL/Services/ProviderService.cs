namespace PremiumLogistic_BAL.Services;

public class ProviderService : IProviderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProviderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ProviderDto>> Get()
    {
        var providers = await _unitOfWork.ProviderRepository.GetAllAsync();
        var result = _mapper.Map<List<ProviderDto>>(providers);
        return result;
    }
}
