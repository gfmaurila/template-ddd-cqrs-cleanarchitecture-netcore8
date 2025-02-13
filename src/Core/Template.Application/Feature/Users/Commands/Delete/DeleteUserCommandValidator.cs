using FluentValidation;

namespace Template.Application.Feature.Users.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("The ID of the Exemple entity must not be empty.");
    }
}
