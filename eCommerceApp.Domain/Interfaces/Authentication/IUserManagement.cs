using eCommerceApp.Domain.Entities.Identity;
using System.Security.Claims;

namespace eCommerceApp.Domain.Interfaces.Authentication;

/// <summary>
/// Provides methods for managing users.
/// </summary>
public interface IUserManagement
{
    Task<bool> CreateUser(AppUser user);
    Task<bool> LoginUser(AppUser user);
    Task<AppUser?> GetUserByEmail(string email);
    Task<AppUser> GetUserById(string id);
    Task<IEnumerable<AppUser>?> GetAllUsers();
    Task<int> RemoveUserByEmail(string email);
    Task<List<Claim>> GetUserClaims(string email);
}
