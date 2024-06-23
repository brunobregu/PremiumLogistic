namespace PremiumLogistic_DomainModels.Models;

public class Contact
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(250)]
    public string Message { get; set; }
}
