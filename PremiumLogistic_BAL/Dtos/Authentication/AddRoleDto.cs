namespace PremiumLogistic_BAL.Dtos.Authentication;

public class AddRoleDto
{
    [Required(ErrorMessage = "RoleRequired")]
    [StringLength(256, ErrorMessage = "RoleMaxChar")]
    public string Name { get; set; }
}
