using eCommerceApp.Application.DTOs;
using FluentValidation;

namespace eCommerceApp.Application.Validations;

/// <summary>
/// Provides validation services for models.
/// </summary>
public class ValidationService : IValidationService
{
    /// <summary>
    /// Validates the specified model using the provided validator.
    /// </summary>
    /// <typeparam name="T">The type of the model to validate.</typeparam>
    /// <param name="model">The model to validate.</param>
    /// <param name="validator">The validator to use for validation.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a <see cref="ServiceResponse"/> indicating the result of the validation.</returns>
    public async Task<ServiceResponse> ValidateAsync<T>(T model,
        IValidator<T> validator)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => e.ErrorMessage).ToList();
            string errorToString = string.Join("; ", errors);
            return new ServiceResponse { Message = errorToString };
        }
        return new ServiceResponse { Success = true };
    }
}
