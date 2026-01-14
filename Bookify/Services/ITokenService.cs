using Bookify.Models;

namespace Bookify.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser user);
    }
}
