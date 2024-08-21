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
    public async Task Add(AddProviderDto addProvider)
    {
        var provider = _mapper.Map<Provider>(addProvider);
        _unitOfWork.ProviderRepository.Insert(provider);
        await _unitOfWork.CommitAsync();
    }

    public async Task Delete(int id)
    {
        var port = await _unitOfWork.ProviderRepository.GetByIdAsync(id) ?? throw new NotFoundException("Provider not found!");
        _unitOfWork.ProviderRepository.Delete(port.Id);
        await _unitOfWork.CommitAsync();
    }
}
