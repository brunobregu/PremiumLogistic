namespace PremiumLogistic_DomainModels.Models;

public class ApplicationRole : IdentityRole
{
    public bool Invalidated { get; set; } = false;
    public DateTime? CreatedOn { get; set; }
    [StringLength(256)]
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    [StringLength(256)]
    public string? UpdatedBy { get; set; }
}
