namespace PremiumLogistic_BAL.Dtos.Authentication;

public class ValidatePasswordRequestDto
{
    [EmailAddress(ErrorMessage = "ValidEmail")]
    public string Email { get; set; }
    [RegularExpression("^[A-Z0-9]{3}-[A-Z0-9]{3}$", ErrorMessage = "TempPassFormat")]
    public string TemporaryPassword { get; set; }
}
