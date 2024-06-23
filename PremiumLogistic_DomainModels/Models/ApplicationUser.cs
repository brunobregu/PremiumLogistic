namespace PremiumLogistic_DomainModels.Models;

public class ApplicationUser : IdentityUser
{
    public bool Invalidated { get; set; } = false;
    public string? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? CreatedIP { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public string? UpdatedIP { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; }
}
