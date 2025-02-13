using Template.Application.Abstractions.Interface;
using Template.Common.Domain.Enumerado;

namespace Template.Application.Feature.Users.Commands.Create;

public record CreateUserCommand(string firstName, string lastName, EGender gender, string email, string phone) : ICommand<Guid>;
