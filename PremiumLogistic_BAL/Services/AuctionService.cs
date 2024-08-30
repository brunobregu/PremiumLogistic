namespace PremiumLogistic_BAL.Services;

public class AuctionService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<Resource> localizer) : IAuctionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<Resource> _localizer = localizer;

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
        var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(id) ?? throw new NotFoundException(_localizer["AuctionNotFound"].Value);
        _unitOfWork.AuctionRepository.Delete(auction.Id);
        await _unitOfWork.CommitAsync();
    }
}
