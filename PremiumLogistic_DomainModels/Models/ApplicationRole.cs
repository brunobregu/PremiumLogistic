namespace PremiumLogistic_DomainModels.Models;

public class ApplicationRole : IdentityRole
{
    public bool Invalidated { get; set; } = false;
    public DateTime? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
}
