namespace PremiumLogistic_DomainModels.Models;

public class AuditLogs
{
    public long Id { get; set; }
    public DateTime? TimeAccessed { get; set; }
    [StringLength(20)]
    public string? IP { get; set; }
    [StringLength(100)]
    public string? Url { get; set; }
    [StringLength(256)]
    public string? AccessedBy { get; set; }
    public string? BodyRequest { get; set; }
}
