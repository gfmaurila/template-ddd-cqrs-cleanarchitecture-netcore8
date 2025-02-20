using CSharpFunctionalExtensions;
using Template.Application.Abstractions.Interface;
using Template.Application.Repositories;
using Template.Common.Domain;
using Template.Common.Domain.Errors;
using Template.Common.Domain.Events;


namespace Template.Application.Abstractions;

public abstract class CommandHandlerBase<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
     where TCommand : ICommand<TResponse>
     where TResponse : notnull
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IUnitOfWork _unitOfWork;

    protected CommandHandlerBase(
        IDomainEventDispatcher domainEventDispatcher,
        IUnitOfWork unitOfWork)
    {
        _domainEventDispatcher = domainEventDispatcher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TResponse, IDomainError>> Handle(TCommand request, CancellationToken cancellationToken)
    {

        // Step 1: Execute core operation
        var operationResult = await ExecuteAsync(request, cancellationToken);
        if (!operationResult.IsSuccess)
            return operationResult;

        // Step 2: Commit Unit of Work
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Step 3: Dispatch Domain Events
        var aggregateRoot = GetAggregateRoot(operationResult);
        if (aggregateRoot != null)
            await DispatchDomainEventsAsync(aggregateRoot.PopDomainEvents(), cancellationToken);

        // Step 4: Return Result
        return operationResult;
    }

    /// <summary>
    /// Executes the core operation logic for the command.
    /// </summary>
    protected abstract Task<Result<TResponse, IDomainError>> ExecuteAsync(TCommand request, CancellationToken cancellationToken);

    /// <summary>
    /// Extracts the aggregate root for dispatching domain events.
    /// </summary>
    protected abstract IAggregateRoot? GetAggregateRoot(Result<TResponse, IDomainError> result);

    /// <summary>
    /// Manually dispatches a collection of domain events.
    /// </summary>
    protected async Task DispatchDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        if (domainEvents == null) return;
        await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);
    }
}
