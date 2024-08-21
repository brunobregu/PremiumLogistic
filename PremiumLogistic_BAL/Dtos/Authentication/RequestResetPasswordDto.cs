namespace PremiumLogistic_BAL.Dtos.Authentication;

public class RequestResetPasswordDto
{
    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress(ErrorMessage = "ValidEmail")]
    public string Email { get; set; }
}
