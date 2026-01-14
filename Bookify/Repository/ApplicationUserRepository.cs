using Bookify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

   public async Task<ApplicationUser?> GetUserByIdAsync(string id)
       => await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

   public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
       => await _userManager.Users.ToListAsync();

   public async Task<bool> AssignRoleAsync(ApplicationUser user, string roleName)
   {
       var result = await _userManager.AddToRoleAsync(user, roleName);
       return result.Succeeded;
   }
}