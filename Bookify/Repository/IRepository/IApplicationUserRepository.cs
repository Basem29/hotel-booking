using Bookify.Models;

public interface IApplicationUserRepository
{
    Task<ApplicationUser?> GetUserByIdAsync(string id);
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<bool> AssignRoleAsync(ApplicationUser user, string roleName);
}
