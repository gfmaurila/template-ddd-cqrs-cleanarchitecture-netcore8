using Microsoft.Extensions.DependencyInjection;
using Template.Application.Abstractions.Interface;
using Template.Common.Domain;
using Template.Common.Domain.Events;

namespace Template.Application.Events.Dispatchers;

public class DomainEventDispatcherWithoutMediatr : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcherWithoutMediatr(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
    {
        var eventQueue = new Queue<IDomainEvent>(events);

        while (eventQueue.Count > 0)
        {
            var domainEvent = eventQueue.Dequeue();
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                var handleMethod = handlerType.GetMethod("Handle");
                if (handleMethod == null) continue;

                await (Task)handleMethod.Invoke(handler, new object[] { domainEvent, cancellationToken });

                // If the handler raises additional events, add them to the queue
                if (domainEvent is IAggregateRoot aggregateRoot)
                {
                    var additionalEvents = aggregateRoot.PopDomainEvents();
                    foreach (var additionalEvent in additionalEvents)
                    {
                        eventQueue.Enqueue(additionalEvent);
                    }
                }
            }
        }
    }

}
