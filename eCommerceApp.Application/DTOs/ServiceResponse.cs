namespace eCommerceApp.Application.DTOs;

/// <summary>
/// Represents a response from a service operation.
/// </summary>
/// <param name="Success">Indicates whether the operation was successful.</param>
/// <param name="Message">Provides a message related to the operation result.</param>
public record ServiceResponse(bool Success = false, string Message = null!);
