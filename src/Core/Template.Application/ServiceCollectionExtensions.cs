using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Abstractions.Interface;
using Template.Application.Behaviours;
using Template.Application.Events.Dispatchers;
using Template.Application.MappingProfiles;
using Template.Common.Domain.Events;

namespace Template.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            configuration.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>));
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });

        // Register all domain event handlers
        services.Scan(scan => scan
            .FromAssemblyOf<IDomainEventHandler<IDomainEvent>>()
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        services.AddAutoMapper(typeof(UserMappingProfile));
        return services;
    }
}
