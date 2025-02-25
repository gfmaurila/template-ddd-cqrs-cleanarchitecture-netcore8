using Confluent.Kafka;

namespace Template.Application.Abstractions.Messaging.Consumer;

/// <summary>
/// Represents the configuration settings for a Kafka consumer.
/// </summary>
public class KafkaConsumerConfig
{
    /// <summary>
    /// Gets or sets the bootstrap servers for the Kafka cluster.
    /// </summary>
    public string BootstrapServers { get; set; }

    /// <summary>
    /// Gets or sets the consumer group ID.
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    /// Gets or sets the auto offset reset behavior, determining where to start consuming messages when no offset is found.
    /// Default is <see cref="AutoOffsetReset.Earliest"/>.
    /// </summary>
    public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;

    /// <summary>
    /// Gets or sets a value indicating whether the consumer should automatically commit offsets.
    /// Default is <c>false</c>.
    /// </summary>
    public bool EnableAutoCommit { get; set; } = false;

    /// <summary>
    /// Gets or sets the session timeout in milliseconds.
    /// If no heartbeat is received within this time, the consumer will be considered dead.
    /// Default is <c>10000</c> milliseconds (10 seconds).
    /// </summary>
    public int SessionTimeoutMs { get; set; } = 10000;

    /// <summary>
    /// Gets or sets the maximum allowed time between calls to poll() before the consumer is considered unresponsive.
    /// Default is <c>300000</c> milliseconds (5 minutes).
    /// </summary>
    public int MaxPollIntervalMs { get; set; } = 300000;
}
