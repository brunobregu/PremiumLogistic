namespace PremiumLogistic_DAL.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly PremiumLogisticDbContext _context;
    public IOceanRepository OceanRepository { get; set; }
    public ITransportationRepository TransportationRepository { get; set; }
    public IAuditLogsRepository AuditLogsRepository { get; set; }
    public IContactRepository ContactRepository {  get; set; }
    public IOrderDetailsRepository OrderDetailsRepository { get; set; }
    public IProviderRepository ProviderRepository { get; set; }
    public IAuctionRepository AuctionRepository { get; set; }
    public IPortRepository PortRepository { get; set; }
    public IUserRepository UserRepository { get; set; }


    public UnitOfWork(PremiumLogisticDbContext context)
    {
        _context = context;
        OceanRepository = new OceanRepository(context);
        TransportationRepository = new TransportationRepository(context);
        AuditLogsRepository = new AuditLogsRepository(context);
        ContactRepository = new ContactRepository(context);
        OrderDetailsRepository = new OrderDetailsRepository(context);
        ProviderRepository = new ProviderRepository(context);
        AuctionRepository = new AuctionRepository(context);
        PortRepository = new PortRepository(context);
        UserRepository = new UserRepository(context);
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
