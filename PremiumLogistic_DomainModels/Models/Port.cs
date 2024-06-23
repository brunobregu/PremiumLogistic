namespace PremiumLogistic_DomainModels.Models;

public class Port : CommonAttributes
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public decimal Price { get; set; }
}
