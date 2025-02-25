using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;
using Template.Application.Abstractions.Messaging.Consumer;
using Template.Application.Abstractions.Messaging.Interface;
using Template.Application.Feature.Users.Commands.Create.Messaging.Consumer;
using Template.Application.Repositories;
using Template.Infrastructure.Messaging;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Persistence.Repositories;

namespace Template.Infrastructure;

public static class InfrastructureExtensions
{
    public static void ApplyMigrations(this IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TemplateAppDbContext>();
                context.Database.Migrate();
                Console.WriteLine("Database migrations applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
                throw;
            }
        }
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Get Kafka configuration from appsettings.json
        var kafkaConfig = configuration.GetSection("Kafka");
        var bootstrapServers = kafkaConfig["BootstrapServers"];
        var defaultTopic = kafkaConfig["DefaultTopic"];
        var groupId = kafkaConfig["GroupId"];

        // Register KafkaIntegrationEventPublisher
        services.AddSingleton<IIntegrationEventPublisher>(sp =>
        {
            return new KafkaIntegrationEventPublisher(bootstrapServers!, defaultTopic!);
        });

        // Configuração do consumidor Kafka
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest, // Para consumir mensagens antigas
            EnableAutoCommit = true,
            SessionTimeoutMs = 6000,  // Reduz o timeout para evitar quedas
            HeartbeatIntervalMs = 2000, // Garante que o consumidor sinalize atividade
            AllowAutoCreateTopics = true // Permite criação automática do tópico
        };

        // Subscribe - Kafka
        // Configuração do Kafka
        services.Configure<KafkaConsumerConfig>(configuration.GetSection("Kafka"));

        // Registrar a configuração como um singleton para ser usada diretamente
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<KafkaConsumerConfig>>().Value);

        // Registrar o KafkaConsumer
        services.AddSingleton<IKafkaConsumer, KafkaConsumer>();

        // Registrar o processador de mensagens como scoped
        services.AddScoped<IUserCreatedMessageProcessor, UserCreatedMessageProcessor>();

        // Registrar o NotificationKafkaSubscribe como hosted service (singleton)
        services.AddHostedService<UserCreatedConsumerEventHandler>();



        // Register DbContext with a connection string
        services.AddDbContext<TemplateAppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        return services;
    }
}
