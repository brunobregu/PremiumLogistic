namespace PremiumLogistic_BAL.IServices;

public interface IUserService
{
    Task Register(RegisterDto registerDto);
    Task<AuthResultDto> Login(LoginDto loginDto);
    Task<List<UsersOfRoleDto>> GetUsersOfRole(string role);
}
