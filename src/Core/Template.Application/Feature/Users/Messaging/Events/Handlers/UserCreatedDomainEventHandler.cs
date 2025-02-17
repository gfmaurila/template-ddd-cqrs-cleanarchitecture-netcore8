﻿using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Messaging.Events.Integration;
using Template.Common.Domain.Events;
using Template.Domain.Users.Events;

namespace Template.Application.Feature.Users.Messaging.Events.Handlers;

public class UserDeleteDomainEventHandler : IDomainEventHandler<UserDeletedEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public UserDeleteDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher)
    {
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task Handle(UserDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserDeleteIntegrationEvent(domainEvent.Id, domainEvent.FirstName, domainEvent.LastName, domainEvent.Gender, domainEvent.Email, domainEvent.Phone);
        await _integrationEventPublisher.PublishAsync(integrationEvent);
    }
}
