namespace PremiumLogistic_BAL.Dtos.Authentication;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress(ErrorMessage = "ValidEmail")]
    public string Email { get; set; }
    [RegularExpression("^[A-Z0-9]{3}-[A-Z0-9]{3}$", ErrorMessage = "TempPassFormat")]
    public string TemporaryPassword { get; set; }
    [Required(ErrorMessage = "PassRequired")]
    public string NewPassword { get; set; }
    [Compare("NewPassword", ErrorMessage = "ComparePass")]
    public string ConfirmNewPassword { get; set; }
}
