using Template.Common.Domain.Events;

namespace Template.Application.Abstractions.Interface;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}