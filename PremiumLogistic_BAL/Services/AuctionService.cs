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

    public async Task Add(AddAuctionDto addAuction)
    {
        var auction = _mapper.Map<Auction>(addAuction);
        _unitOfWork.AuctionRepository.Insert(auction);
        await _unitOfWork.CommitAsync();
    }

    public async Task Delete(int id)
    {
        var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(id) ?? throw new NotFoundException("Auction not found!");
        _unitOfWork.AuctionRepository.Delete(auction.Id);
        await _unitOfWork.CommitAsync();
    }
}
