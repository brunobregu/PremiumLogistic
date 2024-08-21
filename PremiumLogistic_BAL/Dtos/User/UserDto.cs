namespace PremiumLogistic_BAL.Dtos.User;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public IList<string> Role { get; set; }
}
