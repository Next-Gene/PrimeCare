namespace eCommerceApp.Application.DTOs;

/// <summary>
/// Represents the response from a login operation.
/// </summary>
/// <param name="Success">Indicates whether the login operation was successful.</param>
/// <param name="Message">Provides a message related to the login operation result.</param>
/// <param name="Token">The JWT token issued upon successful login.</param>
/// <param name="RefreshToken">The refresh token issued upon successful login.</param>
public record LoginResponse
    (bool Success = false,
    string Message = null!,
    string Token = null!,
    string RefreshToken = null!);
