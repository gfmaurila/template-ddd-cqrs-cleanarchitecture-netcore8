using Confluent.Kafka;
using System.Text.Json;
using Template.Application.Abstractions.Messaging.Interface;
using Template.Common.Core.Events;

namespace Template.Infrastructure.Messaging;

public class KafkaIntegrationEventPublisher : IIntegrationEventPublisher
{
    private readonly IProducer<string, string> _producer;
    private readonly string _defaultTopic;

    public KafkaIntegrationEventPublisher(string bootstrapServers, string defaultTopic)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,
            Acks = Acks.All, // Wait for all replicas to acknowledge
            EnableIdempotence = true, // Ensure exactly-once delivery
            CompressionType = CompressionType.Snappy, // Optimize message size
            LingerMs = 5 // Batch messages for better throughput
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
        _defaultTopic = defaultTopic;
    }

    public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
    {
        // Determine the topic based on the event type
        var topic = _defaultTopic ?? @event.GetType().Name;

        // Serialize the event to JSON
        var message = JsonSerializer.Serialize(@event);

        try
        {
            // Produce the message
            var kafkaMessage = new Message<string, string>
            {
                Key = @event.AggregateId.ToString(),
                Value = message
            };

            var deliveryResult = await _producer.ProduceAsync(topic, kafkaMessage);

            Console.WriteLine($"Delivered event to Kafka: {deliveryResult.TopicPartitionOffset}");
        }
        catch (ProduceException<string, string> ex)
        {
            // Log the exception or handle it as per your needs
            Console.WriteLine($"Failed to deliver event: {ex.Message}");
            throw;
        }
    }

    public async Task PublishAsync<T>(T @event, string? topic = null) where T : IntegrationEvent
    {
        // Usa o tópico fornecido ou o padrão caso nenhum seja especificado
        var targetTopic = topic ?? _defaultTopic ?? @event.GetType().Name;

        // Serializa o evento para JSON
        var message = JsonSerializer.Serialize(@event);

        try
        {
            // Produz a mensagem para o Kafka
            var kafkaMessage = new Message<string, string>
            {
                Key = @event.AggregateId.ToString(),
                Value = message
            };

            var deliveryResult = await _producer.ProduceAsync(targetTopic, kafkaMessage);

            Console.WriteLine($"Delivered event to Kafka topic '{targetTopic}': {deliveryResult.TopicPartitionOffset}");
        }
        catch (ProduceException<string, string> ex)
        {
            Console.WriteLine($"Failed to deliver event to topic '{targetTopic}': {ex.Message}");
            throw;
        }
    }
}
