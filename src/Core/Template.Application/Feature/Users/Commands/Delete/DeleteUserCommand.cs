using Template.Application.Abstractions.Interface;
using Template.Common.Domain.Enumerado;

namespace Template.Application.Feature.Users.Commands.Delete;

public record DeleteUserCommand(Guid Id, string firstName, string lastName, EGender gender, string email, string phone) : ICommand<Guid>;
