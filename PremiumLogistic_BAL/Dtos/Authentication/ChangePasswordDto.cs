namespace PremiumLogistic_BAL.Dtos.Authentication;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Old password is required")]
    public string OldPassword { get; set; }
    [Required(ErrorMessage = "Old password is required")]
    public string NewPassword { get; set; }
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
