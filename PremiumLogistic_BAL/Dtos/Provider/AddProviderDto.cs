namespace PremiumLogistic_BAL.Dtos.Provider;

public class AddProviderDto
{
    [Required(ErrorMessage = "ProviderRequired")]
    [StringLength(50, ErrorMessage = "ProviderChar")]
    public string Name { get; set; }
    [Required(ErrorMessage = "LinkRequired")]
    public string Link { get; set; }
}
