using Template.Application.Abstractions.Messaging.Interface;
using Template.Common.Domain.Events;
using Template.Domain.Users.Events;

namespace Template.Application.Feature.Users.Commands.Create.Events;

public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public UserCreatedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher)
    {
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task Handle(UserCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserCreatedDomainEvent(domainEvent.Id, domainEvent.FirstName, domainEvent.LastName, domainEvent.Gender, domainEvent.Email, domainEvent.Phone);
        await _integrationEventPublisher.PublishAsync(integrationEvent);
    }
}
