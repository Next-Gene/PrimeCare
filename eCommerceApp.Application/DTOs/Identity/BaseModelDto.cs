namespace eCommerceApp.Application.DTOs.Identity;

/// <summary>
/// Represents the base model for user-related data transfer objects.
/// </summary>
public class BaseModelDto
{
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public required string Password { get; set; }
}
