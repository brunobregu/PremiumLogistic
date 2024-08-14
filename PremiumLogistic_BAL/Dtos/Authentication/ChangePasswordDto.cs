namespace PremiumLogistic_BAL.Dtos.Authentication;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "OldPassRequired")]
    public string OldPassword { get; set; }
    [Required(ErrorMessage = "NewPassRequired")]
    public string NewPassword { get; set; }
    [Compare("NewPassword", ErrorMessage = "ComparePass")]
    public string ConfirmPassword { get; set; }
}
