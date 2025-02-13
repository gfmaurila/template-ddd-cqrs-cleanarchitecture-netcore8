using Template.Common.Core.Events;

namespace Template.Application.Abstractions.Messaging.Interface;

public interface IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
