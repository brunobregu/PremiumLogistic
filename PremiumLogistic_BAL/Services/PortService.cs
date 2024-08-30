namespace PremiumLogistic_BAL.Services;

public class PortService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<Resource> localizer) : IPortService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<Resource> _localizer = localizer;

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
        var port = await _unitOfWork.PortRepository.GetByIdAsync(id) ?? throw new NotFoundException(_localizer["PortNotFound"].Value);
        _unitOfWork.PortRepository.Delete(port.Id);
        await _unitOfWork.CommitAsync();
    }
}
