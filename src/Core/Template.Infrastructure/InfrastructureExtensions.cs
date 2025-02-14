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

        // Register KafkaIntegrationEventPublisher
        services.AddSingleton<IIntegrationEventPublisher>(sp =>
        {
            return new KafkaIntegrationEventPublisher(bootstrapServers!, defaultTopic!);
        });

        // Register DbContext with a connection string
        services.AddDbContext<TemplateAppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        return services;
    }
}
