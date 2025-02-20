using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Commands.Create.Messaging.Events;

namespace Template.Application.Feature.Users.Commands.Create.Messaging.Consumer;

public class UserCreatedConsumerEventHandler : IIntegrationEventHandler<UserCreatedDomainEvent>
{
    public Task HandleAsync(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[Kafka] - Evento consumido e processado: {domainEvent.Email}");

        // Aqui você pode salvar no banco de dados, chamar uma API, etc.

        return Task.CompletedTask;
    }
}
