namespace PremiumLogistic_BAL.IServices;

public interface IUserService
{
    Task AddUser(CreateUserDto createUserDto, string email);
    Task Register(RegisterDto registerDto);
    Task<AuthResultDto> Login(LoginDto loginDto);
    Task<List<UsersOfRoleDto>> GetUsersOfRole(string role);
    Task RequestPasswordReset(string email);
    Task ResetPassword(ResetPasswordDto resetPasswordDto);
    Task ChangePassword(ChangePasswordDto changePasswordDto, string email);
    Task<List<RolesDto>> GetRoles();
    Task AddRole(AddRoleDto addRoleDto, string email);
}
