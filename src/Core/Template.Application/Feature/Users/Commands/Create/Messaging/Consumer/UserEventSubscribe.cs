namespace Template.Application.Feature.Users.Commands.Create.Messaging.Subscribe;

//public class UserEventSubscribe
//{
//    private readonly IConsumer<string, string> _consumer;
//    private readonly Dictionary<Type, object> _handlers;
//    private readonly Dictionary<string, Type> _eventTypeMappings;

//    public UserEventSubscribe(
//        IConsumer<string, string> consumer,
//        Dictionary<Type, object> handlers,
//        Dictionary<string, Type> eventTypeMappings)
//    {
//        _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
//        _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
//        _eventTypeMappings = eventTypeMappings ?? throw new ArgumentNullException(nameof(eventTypeMappings));
//    }

//    public async Task StartConsumingAsync(CancellationToken cancellationToken)
//    {
//        var topic = "integration-events";
//        _consumer.Subscribe(topic);

//        Console.WriteLine($"[Kafka] - Iniciando o consumo do tópico: {topic}");

//        try
//        {
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                var consumeResult = _consumer.Consume(cancellationToken);

//                if (consumeResult?.Message?.Value != null)
//                {
//                    var eventType = _eventTypeMappings[topic];
//                    var userEvent = JsonSerializer.Deserialize(consumeResult.Message.Value, eventType);

//                    if (userEvent != null)
//                    {
//                        await DispatchEventAsync(userEvent, cancellationToken);
//                    }
//                }
//            }
//        }
//        catch (OperationCanceledException)
//        {
//            Console.WriteLine("[Kafka] - Consumo interrompido.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"[Kafka] - Erro no consumidor de usuário: {ex.Message}");
//        }
//        finally
//        {
//            _consumer.Close();
//        }
//    }

//    private async Task DispatchEventAsync(object integrationEvent, CancellationToken cancellationToken)
//    {
//        var eventType = integrationEvent.GetType();

//        if (_handlers.TryGetValue(eventType, out var handler))
//        {
//            if (handler is IIntegrationEventHandler<UserCreatedDomainEvent> userCreatedHandler)
//            {
//                await userCreatedHandler.HandleAsync((UserCreatedDomainEvent)integrationEvent, cancellationToken);
//            }
//            else
//            {
//                Console.WriteLine($"[Kafka] - Handler incompatível para o tipo: {eventType.Name}");
//            }
//        }
//        else
//        {
//            Console.WriteLine($"[Kafka] - Nenhum handler encontrado para o tipo: {eventType.Name}");
//        }
//    }
//}
