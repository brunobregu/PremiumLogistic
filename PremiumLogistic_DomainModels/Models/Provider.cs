namespace PremiumLogistic_DomainModels.Models;

public class Provider
{
    public int Id { get; set; }
    public bool Invalidated { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    public string Link { get; set; }
}
