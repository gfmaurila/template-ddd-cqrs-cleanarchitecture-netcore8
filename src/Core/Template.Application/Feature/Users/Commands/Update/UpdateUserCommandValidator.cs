using FluentValidation;
using Template.Common.Domain.Enumerado;

namespace Template.Application.Feature.Users.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.firstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(100)
            .WithMessage("First name must not exceed 100 characters.");

        RuleFor(command => command.lastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(100)
            .WithMessage("Last name must not exceed 100 characters.");

        RuleFor(command => command.email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .MaximumLength(254)
            .WithMessage("Email must not exceed 254 characters.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        // Validation for Gender, ensuring a valid value is selected
        RuleFor(command => command.gender)
            .Must(gender => gender != EGender.None)
            .WithMessage("Please select a valid gender. 'Not specified' is not a permitted option.");
    }
}
