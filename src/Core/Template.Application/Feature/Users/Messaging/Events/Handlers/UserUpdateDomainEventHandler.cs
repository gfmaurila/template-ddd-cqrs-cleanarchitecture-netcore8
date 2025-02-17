using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Messaging.Events.Integration;
using Template.Common.Domain.Events;
using Template.Domain.Users.Events;

namespace Template.Application.Feature.Users.Messaging.Events.Handlers;

public class UserUpdateDomainEventHandler : IDomainEventHandler<UserUpdateEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public UserUpdateDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher)
    {
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task Handle(UserUpdateEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserUpdateIntegrationEvent(domainEvent.Id, domainEvent.FirstName, domainEvent.LastName, domainEvent.Gender, domainEvent.Email, domainEvent.Phone);
        await _integrationEventPublisher.PublishAsync(integrationEvent);
    }
}
