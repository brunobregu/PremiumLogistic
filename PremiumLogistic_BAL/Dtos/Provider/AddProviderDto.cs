namespace PremiumLogistic_BAL.Dtos.Provider;

public class AddProviderDto
{
    [Required(ErrorMessage = "Provider is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Link is required")]
    public string Link { get; set; }
}
