using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Commands.Create.Messaging.Events;
using Template.Common.Domain.Events;
using Template.Domain.Common;
using Template.Domain.Users.Events;

namespace Template.Application.Feature.Users.Commands.Create.Messaging.Producer;

public class UserCreatedProducerEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public UserCreatedProducerEventHandler(IIntegrationEventPublisher integrationEventPublisher)
    {
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task Handle(UserCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[Kafka] - Evento processado pelo handler para: {domainEvent.Email}");

        var integrationEvent = new UserCreatedDomainEvent(domainEvent.Id, domainEvent.FirstName, domainEvent.LastName, domainEvent.Gender, domainEvent.Email, domainEvent.Phone);

        Console.WriteLine($"[Kafka] - Publicando evento para Kafka: {UserEventConstants.UserCreatedTopic}");
        //await _integrationEventPublisher.PublishAsync(integrationEvent);
        await _integrationEventPublisher.PublishAsync(integrationEvent, UserEventConstants.UserCreatedTopic);
    }
}
