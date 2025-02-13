using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Messaging.Events.Integration;

namespace Template.Application.Feature.Users.Messaging;

public class UserValidationFailedHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    public async Task HandleAsync(UserCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User validation failed: Error = {@event.EventType}");
        await Task.CompletedTask;
    }
}
