namespace PremiumLogistic_BAL.IServices;

public interface IAuditLogsService
{
    Task AddLogs(AddAuditLogsDto auditLogs);
}
