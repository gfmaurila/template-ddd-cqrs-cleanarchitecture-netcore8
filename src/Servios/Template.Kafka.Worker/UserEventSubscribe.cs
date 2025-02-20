using Confluent.Kafka;
using System.Text.Json;
using Template.Domain.Common;

namespace Template.Kafka.Worker;

public class UserEventSubscribe
{
    private readonly IConsumer<string, string> _consumer;
    private readonly Dictionary<Type, object> _handlers;
    private readonly Dictionary<string, Type> _eventTypeMappings;

    public UserEventSubscribe(
        IConsumer<string, string> consumer,
        Dictionary<Type, object> handlers,
        Dictionary<string, Type> eventTypeMappings)
    {
        _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        _eventTypeMappings = eventTypeMappings ?? throw new ArgumentNullException(nameof(eventTypeMappings));

        Console.WriteLine($"[Kafka] - Inicializando consumidor. Handlers registrados: {_handlers.Count}");
        Console.WriteLine($"[Kafka] - Inicializando consumidor. Eventos registrados: {_eventTypeMappings.Count}");
    }

    public async Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(UserEventConstants.UserCreatedTopic); // Nome corrigido
        Console.WriteLine($"[Kafka] - Iniciando o consumo do tópico: {UserEventConstants.UserCreatedTopic}");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(cancellationToken);

                Console.WriteLine($"[Kafka] - JSON Recebido: {consumeResult.Message.Value}");

                if (string.IsNullOrEmpty(consumeResult.Message.Value))
                {
                    Console.WriteLine("[Kafka] - Mensagem vazia ou nula recebida.");
                    continue;
                }

                if (!_eventTypeMappings.TryGetValue("UserCreatedDomainEvent", out var eventType))
                {
                    Console.WriteLine($"[Kafka] - Tipo de evento não encontrado: UserCreatedDomainEvent");
                    continue;
                }

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    Console.WriteLine($"[Kafka] - Tentando desserializar o evento como {eventType.Name}");

                    var jsonDoc = JsonDocument.Parse(consumeResult.Message.Value);
                    var jsonElement = jsonDoc.RootElement;

                    var userEvent = JsonSerializer.Deserialize(jsonElement.GetRawText(), eventType, options);

                    if (userEvent == null)
                    {
                        Console.WriteLine("[Kafka] - Falha ao desserializar o evento.");
                        continue;
                    }

                    Console.WriteLine($"[Kafka] - Evento desserializado com sucesso: {JsonSerializer.Serialize(userEvent)}");

                    await DispatchEventAsync(userEvent, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Kafka] - Erro ao desserializar JSON: {ex.Message}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("[Kafka] - Consumo interrompido.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Kafka] - Erro no consumidor de usuário: {ex.Message}");
        }
        finally
        {
            _consumer.Close();
        }
    }

    private async Task DispatchEventAsync(object integrationEvent, CancellationToken cancellationToken)
    {
        var eventType = integrationEvent.GetType();
        Console.WriteLine($"[Kafka] - Tentando despachar evento do tipo: {eventType.Name}");

        if (_handlers.TryGetValue(eventType, out var handler))
        {
            Console.WriteLine($"[Kafka] - Encontrado handler para {eventType.Name}");

            // ✅ Tenta chamar HandleAsync ao invés de Handle
            var handleMethod = handler.GetType().GetMethod("HandleAsync");
            if (handleMethod != null)
            {
                Console.WriteLine($"[Kafka] - Executando handler para {eventType.Name}");
                await (Task)handleMethod.Invoke(handler, new object[] { integrationEvent, cancellationToken });
            }
            else
            {
                Console.WriteLine($"[Kafka] - Nenhum método 'HandleAsync' encontrado no handler para {eventType.Name}");
            }
        }
        else
        {
            Console.WriteLine($"[Kafka] - Nenhum handler encontrado para o tipo: {eventType.Name}");
        }
    }
}

