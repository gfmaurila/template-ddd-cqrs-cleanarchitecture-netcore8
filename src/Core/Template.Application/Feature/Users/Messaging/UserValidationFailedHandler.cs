using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Commands.Create.Messaging.Events;

namespace Template.Application.Feature.Users.Messaging;

public class UserValidationFailedHandler : IIntegrationEventHandler<UserCreatedDomainEvent>
{
    public async Task HandleAsync(UserCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User validation failed: Error = {@event.EventType}");
        await Task.CompletedTask;
    }
}
