namespace Template.Application.Abstractions.Messaging.Interface;

public interface IIntegrationEventConsumer
{
    Task ConsumeAsync(CancellationToken cancellationToken);
}