using Template.Common.Core.Events;

namespace Template.Application.Abstractions.Messaging.Interface;

public interface IIntegrationEventPublisher
{
    Task PublishAsync<T>(T @event) where T : IntegrationEvent;
    Task PublishAsync<T>(T @event, string? topic = null) where T : IntegrationEvent;
}