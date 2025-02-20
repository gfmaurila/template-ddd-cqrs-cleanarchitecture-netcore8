using Confluent.Kafka;
using Template.Application.Feature.Users.Commands.Create.Messaging.Events;
using Template.Application.Feature.Users.Commands.Create.Messaging.Consumer;

namespace Template.Kafka.Worker;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddHostedService<Worker>();

        // Configuração do Kafka
        var kafkaConfig = builder.Configuration.GetSection("Kafka");
        var bootstrapServers = kafkaConfig["BootstrapServers"];
        var groupId = kafkaConfig["GroupId"];

        // Registrar o handler correto
        builder.Services.AddScoped<UserCreatedConsumerEventHandler>();

        // Registrar os handlers corretamente sem sobrescrever
        builder.Services.AddSingleton<Dictionary<Type, object>>(sp =>
        {
            var handlers = new Dictionary<Type, object>
            {
                { typeof(UserCreatedDomainEvent), sp.GetRequiredService<UserCreatedConsumerEventHandler>() }
            };
            return handlers;
        });

        // Registrar mapeamento de eventos (eventTypeMappings)
        builder.Services.AddSingleton<Dictionary<string, Type>>(sp =>
        {
            return new Dictionary<string, Type>
            {
                { "UserCreatedDomainEvent", typeof(UserCreatedDomainEvent) }
            };
        });

        // Configuração do consumidor Kafka
        builder.Services.AddScoped<IConsumer<string, string>>(sp =>
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };
            return new ConsumerBuilder<string, string>(config).Build();
        });

        // Registrar o consumidor de eventos
        builder.Services.AddScoped<UserEventSubscribe>();

        // Inicializar o consumidor Kafka após o registro dos serviços
        var serviceProvider = builder.Services.BuildServiceProvider();
        var userEventConsumer = serviceProvider.GetRequiredService<UserEventSubscribe>();
        var cts = new CancellationTokenSource();
        Task.Run(() => userEventConsumer.StartConsumingAsync(cts.Token));

        var host = builder.Build();
        host.Run();
    }
}
