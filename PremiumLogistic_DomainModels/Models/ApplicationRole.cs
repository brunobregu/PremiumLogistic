namespace PremiumLogistic_DomainModels.Models;

public class ApplicationRole : IdentityRole
{
    public bool Invalidated { get; set; } = false;
    public string? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? CreatedIP { get; set; }
    public string? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public string? UpdatedIP { get; set; }
}
