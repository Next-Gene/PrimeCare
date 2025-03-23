using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerceApp.Infrastructure.Repositories.Authentication;

/// <summary>
/// Provides methods for managing users.
/// </summary>
public class UserManagement : IUserManagement
{
    private readonly IRoleManagement _roleManagement;
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserManagement"/> class.
    /// </summary>
    /// <param name="roleManagement">The role management service.</param>
    /// <param name="userManager">The user manager service.</param>
    /// <param name="dbContext">The database context.</param>
    public UserManagement(IRoleManagement roleManagement,
        UserManager<AppUser> userManager, AppDbContext dbContext)
    {
        _roleManagement = roleManagement;
        _userManager = userManager;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a boolean value indicating whether the user was successfully created.</returns>
    public async Task<bool> CreateUser(AppUser user)
    {
        var _user = await GetUserByEmail(user.Email!);

        if (_user != null) return false;

        return (await _userManager
            .CreateAsync(user!, user!.PasswordHash!)).Succeeded;
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an enumerable collection of users, or null if no users were found.</returns>
    public async Task<IEnumerable<AppUser>?> GetAllUsers()
        => await _dbContext.Users.ToListAsync();

    /// <summary>
    /// Retrieves a user by their email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the user, or null if the user was not found.</returns>
    public async Task<AppUser?> GetUserByEmail(string email)
        => await _userManager.FindByEmailAsync(email);

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the user.</returns>
    public async Task<AppUser> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        return user!;
    }

    /// <summary>
    /// Retrieves the claims of a user based on their email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a list of claims associated with the user.</returns>
    public async Task<List<Claim>> GetUserClaims(string email)
    {

        var _user = await GetUserByEmail(email);

        string? roleName = await _roleManagement.GetUserRole(_user!.Email!);

        List<Claim> claims =
        [
            new Claim("Fullname", _user!.Fullname),
            new Claim(ClaimTypes.NameIdentifier, _user!.Id),
            new Claim(ClaimTypes.Email, _user!.Email!),
            new Claim(ClaimTypes.Role, roleName!)
        ];

        return claims;
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="user">The user to log in.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a boolean value indicating whether the user was successfully logged in.</returns>
    public async Task<bool> LoginUser(AppUser user)
    {
        var _user = await GetUserByEmail(user.Email!);

        if (_user is null) return false;

        string? roleName = await _roleManagement.GetUserRole(_user!.Email!);

        if (string.IsNullOrEmpty(roleName)) return false;

        return await _userManager.CheckPasswordAsync(_user, user.PasswordHash!);
    }

    /// <summary>
    /// Removes a user by their email.
    /// </summary>
    /// <param name="email">The email of the user to remove.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an integer indicating the result of the operation.</returns>
    public async Task<int> RemoveUserByEmail(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        _dbContext.Users.Remove(user);

        return await _dbContext.SaveChangesAsync();
    }
}
