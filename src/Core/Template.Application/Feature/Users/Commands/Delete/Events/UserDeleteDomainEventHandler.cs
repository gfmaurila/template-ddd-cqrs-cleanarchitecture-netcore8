using Template.Application.Abstractions.Messaging.Interface;
using Template.Common.Domain.Events;
using Template.Domain.Users.Events;

namespace Template.Application.Feature.Users.Commands.Delete.Events;

public class UserDeleteDomainEventHandler : IDomainEventHandler<UserDeletedEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public UserDeleteDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher)
    {
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task Handle(UserDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserDeleteDomainEvent(domainEvent.Id, domainEvent.FirstName, domainEvent.LastName, domainEvent.Gender, domainEvent.Email, domainEvent.Phone);
        await _integrationEventPublisher.PublishAsync(integrationEvent);
    }
}
