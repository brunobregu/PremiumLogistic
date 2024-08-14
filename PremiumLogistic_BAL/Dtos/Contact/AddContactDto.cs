namespace PremiumLogistic_BAL.Dtos.Contact;

public class AddContactDto
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
    [Required(ErrorMessage = "MessageRequired")]
    [StringLength(250, ErrorMessage = "MessageMaxChar")]
    public string Message { get; set; }
}
