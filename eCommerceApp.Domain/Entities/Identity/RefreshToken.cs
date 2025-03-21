namespace eCommerceApp.Domain.Entities.Identity;

/// <summary>
/// Represents a refresh token used for authentication.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Gets or sets the unique identifier for the refresh token.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the user ID associated with the refresh token.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the token string.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}
