using eCommerceApp.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerceApp.Application.Validations.Authentication;

public class CreateUserValidater : AbstractValidator<CreateUser>
{
    public CreateUserValidater()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full Name is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]")
            .WithMessage("Password must contain at least one upper letter.")
            .Matches(@"[a-z]")
            .WithMessage("Password must contain at least one lower letter.")
            .Matches(@"\d")
            .WithMessage("Password must contain at least one number.")
            .Matches(@"[^\w]")
            .WithMessage("Password must contain at least one special charcter.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Password do not match.");
    }
}

public class LoginUserValidater : AbstractValidator<LoginUser>
{
    public LoginUserValidater()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}
