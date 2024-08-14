namespace PremiumLogistic_BAL.Dtos.Authentication;

public class CreateUserDto
{
    [Required(ErrorMessage = "FNRequired")]
    [StringLength(50, ErrorMessage = "FNMaxChar")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "LNRequired")]
    [StringLength(50, ErrorMessage = "LNMaxChar")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress(ErrorMessage = "ValidEmail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "PassRequired")]
    public string Password { get; set; }
    [Required(ErrorMessage = "RoleRequired")]
    public string RoleName { get; set; }
}
