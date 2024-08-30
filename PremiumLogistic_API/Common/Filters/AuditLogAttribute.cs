namespace PremiumLogistic_API.Common.Filters;

public class AuditLogAttribute(IAuditLogsService auditLogsService) : ActionFilterAttribute
{
    private readonly IAuditLogsService _auditLogsService = auditLogsService;

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
                IP = request.HttpContext.Connection.RemoteIpAddress.ToString() ?? "::1",
                BodyRequest = JsonConvert.SerializeObject(context.ActionArguments)
            };
            await _auditLogsService.AddLogs(auditLog);
        }
        await next();
    }
}
