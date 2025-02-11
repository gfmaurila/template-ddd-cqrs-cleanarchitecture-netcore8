using MediatR;

namespace Template.Common.Domain.Events;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
{ }
