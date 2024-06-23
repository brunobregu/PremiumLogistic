namespace PremiumLogistic_BAL.Dtos.Authentication;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email should be a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
