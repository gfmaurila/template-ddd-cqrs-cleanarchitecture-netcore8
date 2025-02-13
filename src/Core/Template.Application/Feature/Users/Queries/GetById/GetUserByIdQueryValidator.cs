using FluentValidation;

namespace Template.Application.Feature.Users.Queries.GetById;

/// <summary>
/// Validator for the <see cref="GetUserByIdQueryValidator"/> class, ensuring that the provided ID is valid.
/// </summary>
public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetExempleByIdQueryValidator"/> class.
    /// Defines validation rules for retrieving an Exemple entity by its ID.
    /// </summary>
    public GetUserByIdQueryValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("The ID of the Exemple entity must not be empty.");
    }
}
