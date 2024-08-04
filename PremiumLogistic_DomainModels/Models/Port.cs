namespace PremiumLogistic_DomainModels.Models;

public class Port
{
    public int Id { get; set; }
    public bool Invalidated { get; set; }
    [Required]
    public string Name { get; set; }
}
