namespace PremiumLogistic_BAL.Services;

public class AuctionService : IAuctionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AuctionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    
    public async Task<List<AuctionDto>> Get()
    {
        var auctions = await _unitOfWork.AuctionRepository.GetAllAsync();
        var result = _mapper.Map<List<AuctionDto>>(auctions);
        return result;
    }
}
