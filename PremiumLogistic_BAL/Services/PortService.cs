namespace PremiumLogistic_BAL.Services;

public class PortService : IPortService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PortService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<PortDto>> GetPorts()
    {
        var ports = await _unitOfWork.PortRepository.GetAllAsync();
        var result = _mapper.Map<List<PortDto>>(ports);
        await _unitOfWork.CommitAsync();
        return result;
    }
}
