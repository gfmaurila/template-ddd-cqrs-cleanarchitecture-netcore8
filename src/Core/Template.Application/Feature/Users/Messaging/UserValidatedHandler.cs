using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Commands.Create.Messaging.Events;

namespace Template.Application.Feature.Users.Messaging;

public class UserValidatedHandler : IIntegrationEventHandler<UserCreatedDomainEvent>
{
    public Task HandleAsync(UserCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
