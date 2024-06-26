namespace PremiumLogistic_BAL.Dtos.Authentication;

public class AddRoleDto
{
    [Required(ErrorMessage = "Role name is required.")]
    [StringLength(256, ErrorMessage = "Role name must be at most 256 characters.")]
    public string Name { get; set; }
}
