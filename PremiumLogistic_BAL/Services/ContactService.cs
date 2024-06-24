namespace PremiumLogistic_BAL.Services;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task AddContact(AddContactDto addContactDto)
    {
        var contacts = _mapper.Map<Contact>(addContactDto);
        _unitOfWork.ContactRepository.Insert(contacts);
        await _unitOfWork.CommitAsync();
    }
}
