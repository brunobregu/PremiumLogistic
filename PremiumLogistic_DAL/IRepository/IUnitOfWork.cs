namespace PremiumLogistic_DAL.IRepository;

public interface IUnitOfWork
{
    IPortRepository PortRepository { get; }
    ILocalTransportationRepository LocalTransportationRepository { get; }
    IAuditLogsRepository AuditLogsRepository { get; }
    IContactRepository ContactRepository { get; }
    IOrderDetailsRepository OrderDetailsRepository {  get; }
    void Dispose();
    Task<int> CommitAsync();
    int Commit();
}
