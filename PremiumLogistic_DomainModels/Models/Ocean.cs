namespace PremiumLogistic_DomainModels.Models;

public class Ocean
{
    public int Id { get; set; }
    public bool Invalidated { get; set; } = false;
    [Required]
    [StringLength(50)]
    public string Port { get; set; }
    [Range(0, Int32.MinValue)]
    public int Savannah { get; set; }
    [Range(0, Int32.MinValue)]
    public int Elizabeth { get; set; }
    [Range(0, Int32.MinValue)]
    public int Houston { get; set; }
    [Range(0, Int32.MinValue)]
    public int LosAngeles { get; set; }
    [Range(0, Int32.MinValue)]
    public int Indianapolis { get; set; }
}
