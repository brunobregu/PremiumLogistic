namespace PremiumLogistic_BAL.Dtos.Authentication;

public class LoginDto
{
    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress(ErrorMessage = "ValidEmail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "PassRequired")]
    public string Password { get; set; }
}
