using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Template.Domain.Users.Events;

namespace Template.Application.Feature.Users.Commands.Create.Messaging.Consumer;

public class UserCreatedMessageProcessor : IUserCreatedMessageProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserCreatedMessageProcessor> _logger;

    public UserCreatedMessageProcessor(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<UserCreatedMessageProcessor> logger)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task ProcessMessageAsync(string message)
    {
        try
        {
            var messageEvent = JsonConvert.DeserializeObject<UserCreatedEvent>(message);

            if (messageEvent != null)
            {
                //using var scope = _scopeFactory.CreateScope();
                //var messageService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                _logger.LogInformation("UserCreatedMessageProcessor > ProcessMessageAsync > .......");


                //await message.NotificationAsync(request);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing message: {ex.Message}");
        }
    }
}

