namespace PremiumLogistic_BAL.Dtos.Authentication;

public class ValidatePasswordRequestDto
{
    [EmailAddress(ErrorMessage = "Email is not a valid email")]
    public string Email { get; set; }
    [RegularExpression("^[A-Z0-9]{3}-[A-Z0-9]{3}$", ErrorMessage = "Invalid password format. Password must be in the format XXX-XXX where X is an uppercase letter or a number.")]
    public string TemporaryPassword { get; set; }
}
