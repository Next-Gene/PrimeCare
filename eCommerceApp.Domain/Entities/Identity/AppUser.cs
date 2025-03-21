using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Domain.Entities.Identity;

/// <summary>
/// Represents an application user.
/// </summary>
public class AppUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string Fullname { get; set; } = string.Empty;
}