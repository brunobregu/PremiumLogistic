namespace PremiumLogistic_BAL.Dtos.Provider;

public class AddProviderDto
{
    [Required(ErrorMessage = "Provider is required")]
    [StringLength(50, ErrorMessage = "Provider name should be at most 50 characteres")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Link is required")]
    public string Link { get; set; }
}
