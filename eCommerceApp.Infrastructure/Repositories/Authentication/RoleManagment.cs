using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    // <summary>
    // / Provides a class for managing user roles
    public class RoleManagment(UserManager<AppUser> userManager) : IRoleManagement
    {
        // <summary>
        // / Adds a user to a role.
        public async Task<bool> AddUserToRole(AppUser user, string roleName) =>
            (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

        // <summary>
        // / Gets the role of a user.
        public async Task<string?> GetUserRole(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
        }
    }

    // <summary>
    // / Provides a class for managing users.
    public class UserManagement(IRoleManagement roleManagement, UserManager<AppUser> userManager, AppDbContext context) : IUserManagement
    {
        // <summary>
        // / Creates a user.
        public async Task<bool> CreateUser(AppUser user)
        {
            AppUser? _user = await GetUserByEmail(user.Email!);
            // Check if the user already exists
            if (user != null) return false;
            // Create the user
            return (await userManager.CreateAsync(user!, user!.PasswordHash!)).Succeeded;
        }

        // <summary>
        // / Gets all users.
        public async Task<IEnumerable<AppUser>?> GetAllUsers() => await context.Users.ToListAsync();

        // <summary>
        // / Gets a user by email.
        public async Task<AppUser?> GetUserByEmail(string email) =>
            await userManager.FindByEmailAsync(email);

        // <summary>
        // / Gets a user by ID.
        public async Task<AppUser> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user!;
        }

        // <summary>
        // / Gets the claims of a user.
        public async Task<List<Claim>> GetUserClaims(string email)
        {
            // Get the user
            var _user = await GetUserByEmail(email);
            // Get the user's role
            string? roleName = await roleManagement.GetUserRole(_user!.Email!);
            // Create the claims
            List<Claim> claims =
            [
                new Claim("Fullname", _user!.Fullname),
                new Claim(ClaimTypes.NameIdentifier, _user!.Id),
                new Claim(ClaimTypes.Email, _user!.Email!),
                new Claim(ClaimTypes.Role, roleName!)
            ]; // Return the claims
            return claims;
        }

        // <summary>
        // / Logs in a user.
        public async Task<bool> LoginUser(AppUser user)
        {
            // Get the user
            var _user = await GetUserByEmail(user.Email!);
            // Check if the user exists
            if (_user is null) return false;
            // Get the user's role
            string? roleNmae = await roleManagement.GetUserRole(_user!.Email!);
            // Check if the role is empty
            if (string.IsNullOrEmpty(roleNmae)) return false;
            // Check the password
            return await userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        // <summary>
        // / Removes a user by email.
        public async Task<int> RemoveUserByEmail(string email)
        {
            // Get the user
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            // Remove the user
            context.Users.Remove(user);
            // Save the changes
            return await context.SaveChangesAsync();
        }
    }
}
