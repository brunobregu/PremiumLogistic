namespace PremiumLogistic_DomainModels.Models;

public class Port
{
    public int Id { get; set; }
    public bool Invalidated { get; set; } = false;
    public DateTime? CreatedOn { get; set; }
    [StringLength(50)]
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    [StringLength(50)]
    public string? UpdatedBy { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public decimal Price { get; set; }
}
