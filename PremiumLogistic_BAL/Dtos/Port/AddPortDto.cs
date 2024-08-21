namespace PremiumLogistic_BAL.Dtos.Port;

public class AddPortDto
{
    [Required(ErrorMessage = "Port is required")]
    [StringLength(50, ErrorMessage = "Port name should be at most 50 characteres")]
    public string Name { get; set; }
}
