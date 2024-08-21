namespace PremiumLogistic_BAL.Services;

public class ContactService(IUnitOfWork unitOfWork, IMapper mapper) : IContactService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task Add(AddContactDto addContactDto)
    {
        var contacts = _mapper.Map<Contact>(addContactDto);
        _unitOfWork.ContactRepository.Insert(contacts);
        await _unitOfWork.CommitAsync();
    }

    public async Task<List<AllContactsDto>> All()
    {
        var contacts = await _unitOfWork.ContactRepository.GetAllAsync();
        var result = _mapper.Map<List<AllContactsDto>>(contacts);
        return result;
    }
}
