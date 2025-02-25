using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Template.Application.Abstractions.Messaging.Consumer;
using Template.Domain.Common;

namespace Template.Application.Feature.Users.Commands.Create.Messaging.Consumer;

public class UserCreatedConsumerEventHandler : IHostedService
{
    private readonly ILogger<UserCreatedConsumerEventHandler> _logger;
    private readonly IKafkaConsumer _kafkaConsumer;
    private readonly IServiceScopeFactory _scopeFactory;

    public UserCreatedConsumerEventHandler(
        IKafkaConsumer kafkaConsumer,
        IServiceScopeFactory scopeFactory,
        ILogger<UserCreatedConsumerEventHandler> logger)
    {
        _kafkaConsumer = kafkaConsumer;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Kafka Consumer...");

        Task.Run(() => ConsumeMessagesAsync(cancellationToken), cancellationToken);

        return Task.CompletedTask;
    }

    private async Task ConsumeMessagesAsync(CancellationToken cancellationToken)
    {
        await _kafkaConsumer.ConsumeAsync(
            new List<string> { UserEventConstants.UserCreatedTopic },
            UserEventConstants.UserCreatedGroupId,
            async message =>
            {
                using var scope = _scopeFactory.CreateScope();
                var messageProcessor = scope.ServiceProvider.GetRequiredService<IUserCreatedMessageProcessor>();
                await messageProcessor.ProcessMessageAsync(message);
            },
            cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Kafka Consumer...");
        return Task.CompletedTask;
    }
}