using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Domain.Interfaces.Authentication;

/// <summary>
/// Provides methods for managing user roles.
/// </summary>
public interface IRoleManagement
{
    Task<string?> GetUserRole(string userEmail);
    Task<bool> AddUserToRole(AppUser user, string roleName);
}
