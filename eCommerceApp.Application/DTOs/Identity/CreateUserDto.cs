namespace eCommerceApp.Application.DTOs.Identity;

/// <summary>
/// Represents the data transfer object for creating a new user.
/// </summary>
public class CreateUserDto : BaseModelDto
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the confirmation password of the user.
    /// </summary>
    public required string ConfirmPassword { get; set; }
}
