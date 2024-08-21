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
    public async Task<List<PortDto>> Get()
    {
        var ports = await _unitOfWork.PortRepository.GetAllAsync();
        var result = _mapper.Map<List<PortDto>>(ports);
        return result;
    }

    public async Task Add(AddPortDto addPort)
    {
        var port = _mapper.Map<Port>(addPort);
        _unitOfWork.PortRepository.Insert(port);
        await _unitOfWork.CommitAsync();
    }

    public async Task Delete(int id)
    {
        var port = await _unitOfWork.PortRepository.GetByIdAsync(id) ?? throw new NotFoundException("Port not found");
        _unitOfWork.PortRepository.Delete(port.Id);
        await _unitOfWork.CommitAsync();
    }
}
