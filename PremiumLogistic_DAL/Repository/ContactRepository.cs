namespace PremiumLogistic_DAL.Repository;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(PremiumLogisticDbContext context) : base(context) { }
}
