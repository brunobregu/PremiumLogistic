namespace PremiumLogistic_BAL.Dtos.Contact;

public class AddContactDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name should be at most 50 characters")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name should be at most 50 characters")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email should be a valid email address")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Message is required")]
    [StringLength(250, ErrorMessage = "Message should be at most 250 characters")]
    public string Message { get; set; }
}
