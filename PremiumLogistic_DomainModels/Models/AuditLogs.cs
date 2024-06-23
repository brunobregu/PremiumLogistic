namespace PremiumLogistic_DomainModels.Models;

public class AuditLogs
{
    public long Id { get; set; }
    public DateTime? TimeAccessed { get; set; }
    public string? IP { get; set; }
    public string? Url { get; set; }
    public string? AccessedBy { get; set; }
    public string? BodyRequest { get; set; }
}
