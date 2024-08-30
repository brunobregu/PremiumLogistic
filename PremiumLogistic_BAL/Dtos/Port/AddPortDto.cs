namespace PremiumLogistic_BAL.Dtos.Port;

public class AddPortDto
{
    [Required(ErrorMessage = "PortRequired")]
    [StringLength(50, ErrorMessage = "PortChar")]
    public string Name { get; set; }
}
