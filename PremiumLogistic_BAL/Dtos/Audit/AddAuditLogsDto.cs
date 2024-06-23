namespace PremiumLogistic_BAL.Dtos.Audit;

public class AddAuditLogsDto
{
    public DateTime TimeAccessed { get; set; }
    public string IP { get; set; }
    public string Url { get; set; }
    public string AccessedBy { get; set; }
}
