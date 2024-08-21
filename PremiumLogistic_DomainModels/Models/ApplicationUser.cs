namespace PremiumLogistic_DomainModels.Models;

public class ApplicationUser : IdentityUser
{
    public bool Invalidated { get; set; } = false;
    public DateTime? CreatedOn { get; set; }
    [StringLength(256)]
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    [StringLength(256)]
    public string? UpdatedBy { get; set; }
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    [StringLength(20)]
    public string? TemporaryPassword { get; set; }
    public DateTime? TemporaryPasswordExpiration { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; }
}
