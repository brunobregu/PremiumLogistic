namespace PremiumLogistic_BAL.IServices;

public interface IContactService
{
    Task Add(AddContactDto addContactDto);
    Task<List<AllContactsDto>> All();
}
