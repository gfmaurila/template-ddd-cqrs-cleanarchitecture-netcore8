using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Template.Application.Abstractions.Messaging.Consumer;

/// <summary>
/// Represents a Kafka consumer that subscribes to specified topics and processes incoming messages asynchronously.
/// </summary>
public class KafkaConsumer : IKafkaConsumer
{
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly KafkaConsumerConfig _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaConsumer"/> class.
    /// </summary>
    /// <param name="config">Configuration settings for the Kafka consumer.</param>
    /// <param name="logger">Logger instance for logging information and errors.</param>
    public KafkaConsumer(KafkaConsumerConfig config, ILogger<KafkaConsumer> logger)
    {
        _logger = logger;
        _config = config;
    }

    /// <summary>
    /// Starts consuming messages from the specified Kafka topics asynchronously.
    /// </summary>
    /// <param name="topics">A collection of Kafka topics to subscribe to.</param>
    /// <param name="groupId">The consumer group ID.</param>
    /// <param name="messageHandler">A function to handle each consumed message.</param>
    /// <param name="cancellationToken">A token to cancel the consuming process.</param>
    /// <returns>A task that represents the asynchronous consume operation.</returns>
    public async Task ConsumeAsync(IEnumerable<string> topics, string groupId, Func<string, Task> messageHandler, CancellationToken cancellationToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _config.BootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = _config.AutoOffsetReset,
            EnableAutoCommit = _config.EnableAutoCommit,
            SessionTimeoutMs = _config.SessionTimeoutMs,
            MaxPollIntervalMs = _config.MaxPollIntervalMs
        };

        using var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        consumer.Subscribe(topics);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(cancellationToken);

                if (consumeResult != null)
                {
                    _logger.LogInformation($"Received message from {consumeResult.Topic}: {consumeResult.Message.Value}");
                    await messageHandler(consumeResult.Message.Value);
                    consumer.Commit(consumeResult);
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Kafka Consumer stopped.");
        }
        finally
        {
            consumer.Close();
        }
    }
}
