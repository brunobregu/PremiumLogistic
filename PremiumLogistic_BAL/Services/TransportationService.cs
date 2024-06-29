namespace PremiumLogistic_BAL.Services;

public class TransportationService : ITransportationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TransportationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<TransportationDto> GetPrice(string zip, string terminal)
    {
        var localPrices = await _unitOfWork.TransportationRepository.GetAsync(x => x.Zip == zip) ?? throw new NotFoundException("Zip doesn't exist");
        var oceanPrices = await _unitOfWork.OceanRepository.GetAsync(x => x.Port == terminal) ?? throw new NotFoundException("Terminal doesn't exist");
        var transportationDto = new TransportationDto
        {
            Savannah = CreatePriceDto(localPrices.Savannah, oceanPrices.Savannah),
            Elizabeth = CreatePriceDto(localPrices.Elizabeth, oceanPrices.Elizabeth),
            Houston = CreatePriceDto(localPrices.Houston, oceanPrices.Houston),
            LosAngeles = CreatePriceDto(localPrices.LosAngeles, oceanPrices.LosAngeles),
            Indianapolis = CreatePriceDto(localPrices.Indianapolis, oceanPrices.Indianapolis)
        };

        return transportationDto;
    }

    public static PriceDto CreatePriceDto(int landPrice, int oceanPrice)
    {
        return new PriceDto
        {
            Land = landPrice,
            Ocean = oceanPrice
        };
    }
}
