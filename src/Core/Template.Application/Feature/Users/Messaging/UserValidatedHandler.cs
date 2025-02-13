using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Messaging.Events.Integration;

namespace Template.Application.Feature.Users.Messaging;

public class UserValidatedHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    public Task HandleAsync(UserCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
