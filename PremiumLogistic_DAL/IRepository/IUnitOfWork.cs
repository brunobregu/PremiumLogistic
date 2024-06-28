namespace PremiumLogistic_DAL.IRepository;

public interface IUnitOfWork
{
    IOceanRepository OceanRepository { get; }
    ITransportationRepository TransportationRepository { get; }
    IAuditLogsRepository AuditLogsRepository { get; }
    IContactRepository ContactRepository { get; }
    IOrderDetailsRepository OrderDetailsRepository {  get; }
    void Dispose();
    Task<int> CommitAsync();
    int Commit();
}
