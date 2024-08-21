namespace PremiumLogistic_BAL.Dtos.Authentication;

public class RegisterDto
{
    [Required(ErrorMessage = "FNRequired")]
    [StringLength(50, ErrorMessage = "FNMaxChar")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "LNRequired")]
    [StringLength(50, ErrorMessage = "LNMaxChar")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress(ErrorMessage = "ValidEmail")]
    [StringLength(256, ErrorMessage = "Email should be at most 256 characteres")]
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "PassRequired")]
    public string Password { get; set; }
}
