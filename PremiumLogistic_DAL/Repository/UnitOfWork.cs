namespace PremiumLogistic_DAL.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly PremiumLogisticDbContext _context;
    public IPortRepository PortRepository { get; set; }
    public ILocalTransportationRepository LocalTransportationRepository { get; set; }
    public IAuditLogsRepository AuditLogsRepository { get; set; }
    public IContactRepository ContactRepository {  get; set; }
    public IUserRepository AuthenticationRepository { get; set; }
    public IOrderDetailsRepository OrderDetailsRepository { get; set; }


    public UnitOfWork(PremiumLogisticDbContext context)
    {
        _context = context;
        PortRepository = new PortRepository(context);
        LocalTransportationRepository = new LocalTransportationRepository(context);
        AuditLogsRepository = new AuditLogsRepository(context);
        ContactRepository = new ContactRepository(context);
        AuthenticationRepository = new UserRepository(context);
        OrderDetailsRepository = new OrderDetailsRepository(context);
    }
    public int Commit()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
