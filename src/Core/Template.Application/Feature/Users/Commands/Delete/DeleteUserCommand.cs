using Template.Application.Abstractions.Interface;

namespace Template.Application.Feature.Users.Commands.Delete;

public record DeleteUserCommand(Guid Id) : ICommand<Guid>;
