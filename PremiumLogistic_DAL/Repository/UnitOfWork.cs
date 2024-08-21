namespace PremiumLogistic_DAL.Repository;

public class UnitOfWork(PremiumLogisticDbContext context) : IUnitOfWork
{
    private readonly PremiumLogisticDbContext _context = context;
    public IOceanRepository OceanRepository { get; set; } = new OceanRepository(context);
    public ITransportationRepository TransportationRepository { get; set; } = new TransportationRepository(context);
    public IAuditLogsRepository AuditLogsRepository { get; set; } = new AuditLogsRepository(context);
    public IContactRepository ContactRepository { get; set; } = new ContactRepository(context);
    public IOrderDetailsRepository OrderDetailsRepository { get; set; } = new OrderDetailsRepository(context);
    public IProviderRepository ProviderRepository { get; set; } = new ProviderRepository(context);
    public IAuctionRepository AuctionRepository { get; set; } = new AuctionRepository(context);
    public IPortRepository PortRepository { get; set; } = new PortRepository(context);
    public IUserRepository UserRepository { get; set; } = new UserRepository(context);

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
