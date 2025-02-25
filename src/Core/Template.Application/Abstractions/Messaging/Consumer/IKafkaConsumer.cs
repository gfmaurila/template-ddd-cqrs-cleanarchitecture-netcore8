namespace Template.Application.Abstractions.Messaging.Consumer;

public interface IKafkaConsumer
{
    Task ConsumeAsync(IEnumerable<string> topics, string groupId, Func<string, Task> messageHandler, CancellationToken cancellationToken);
}
