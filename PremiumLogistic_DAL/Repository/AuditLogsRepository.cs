namespace PremiumLogistic_DAL.Repository;

public class AuditLogsRepository : Repository<AuditLogs>, IAuditLogsRepository
{
    public AuditLogsRepository(PremiumLogisticDbContext context) : base(context) { }
}
