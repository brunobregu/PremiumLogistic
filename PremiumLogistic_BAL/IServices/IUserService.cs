namespace PremiumLogistic_BAL.IServices;

public interface IUserService
{
    Task<string> AddUser(CreateUserDto createUserDto, string email);
    Task Register(RegisterDto registerDto);
    Task<AuthResultDto> Login(LoginDto loginDto);
    Task<List<UsersOfRoleDto>> GetUsersOfRole(string role);
    Task<string> RequestPasswordReset(string email);
    Task ResetPassword(ResetPasswordDto resetPasswordDto);
    Task ChangePassword(ChangePasswordDto changePasswordDto, string email);
    Task<List<RolesDto>> GetRoles();
    Task AddRole(AddRoleDto addRoleDto, string email);
    Task<List<UserDto>> ActiveUsers(string email);
    Task<List<UserDto>> NonActiveUsers();
    Task DeleteUser(string id, string email);
    Task ActivateUser(string id, string email, string role);
    Task DeleteRole(string role);
    Task<PersonalDataDto> PersonalData(string email);
}
