namespace PremiumLogistic_API.Common.Filters;

public class AuditLogAttribute : ActionFilterAttribute
{
    private readonly IAuditLogsService _auditLogsService;
    public AuditLogAttribute(IAuditLogsService auditLogsService)
    {
        _auditLogsService = auditLogsService;
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context is not null)
        {
            var request = context.HttpContext.Request;

            var auditLog = new AddAuditLogsDto
            {
                TimeAccessed = DateTime.Now,
                AccessedBy = context.HttpContext.User.Identity.Name ?? "Anonymous",
                Url = request.Path,
                IP = request.HttpContext.Connection.RemoteIpAddress?.ToString()
            };
            await _auditLogsService.AddLogs(auditLog);
        }
        await next();
    }
}
