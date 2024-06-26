namespace PremiumLogistic_BAL.Dtos.Authentication;

public class CreateUserDto
{
    [Required(ErrorMessage = "Firstname is required")]
    [StringLength(50, ErrorMessage = "Firstname should be at most 50 characters")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Lastname is required")]
    [StringLength(50, ErrorMessage = "Lastname should be at most 50 characters")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email should be a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Role is required")]
    public string RoleName { get; set; }
}
