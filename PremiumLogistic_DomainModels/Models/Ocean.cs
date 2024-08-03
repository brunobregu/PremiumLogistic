namespace PremiumLogistic_DomainModels.Models;

public class Ocean
{
    public int Id { get; set; }
    public bool Invalidated { get; set; } = false;
    [Required]
    [StringLength(50)]
    public string Port { get; set; }
    [Required]
    public int Savannah { get; set; }
    [Required]
    public int Elizabeth { get; set; }
    [Required]
    public int Houston { get; set; }
    [Required]
    public int LosAngeles { get; set; }
    [Required]
    public int Indianapolis { get; set; }
}
