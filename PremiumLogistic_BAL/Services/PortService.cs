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
}
