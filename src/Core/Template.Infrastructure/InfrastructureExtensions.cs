using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Template.Application.Abstractions.Messaging.Interface;
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


        //// Registrar os dicionários para o consumidor
        //services.AddSingleton<Dictionary<Type, object>>(sp => new Dictionary<Type, object>());
        //services.AddSingleton<Dictionary<string, Type>>(sp =>
        //{
        //    var mappings = new Dictionary<string, Type>
        //    {
        //        { "integration-events", typeof(UserCreatedDomainEvent) }, // Ajuste conforme o tipo do evento
        //        //{ "user-events", typeof(UserCreatedDomainEvent) }
        //    };
        //    return mappings;
        //});

        //// Registrar o consumidor Kafka como Scoped
        //services.AddScoped<IConsumer<string, string>>(sp =>
        //{
        //    var config = new ConsumerConfig
        //    {
        //        BootstrapServers = bootstrapServers,
        //        GroupId = groupId,
        //        AutoOffsetReset = AutoOffsetReset.Earliest,
        //        EnableAutoCommit = true
        //    };
        //    return new ConsumerBuilder<string, string>(config).Build();
        //});

        //// Registrar o IntegrationEventConsumer
        //services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();

        // Registrar o consumidor específico para eventos de usuário
        //services.AddScoped<UserEventSubscribe>();

        // Register DbContext with a connection string
        services.AddDbContext<TemplateAppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));


        // Inicializar o consumidor Kafka após o registro dos serviços
        //var serviceProvider = services.BuildServiceProvider();
        //var userEventConsumer = serviceProvider.GetRequiredService<UserEventSubscribe>();
        //var cts = new CancellationTokenSource();
        //Task.Run(() => userEventConsumer.StartConsumingAsync(cts.Token));

        return services;
    }
}
