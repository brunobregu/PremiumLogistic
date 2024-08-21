namespace PremiumLogistic_BAL.Dtos.Port;

public class AddPortDto
{
    [Required(ErrorMessage = "Port is required")]
    public string Name { get; set; }
}
