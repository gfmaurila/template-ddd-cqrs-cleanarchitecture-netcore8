using CSharpFunctionalExtensions;
using Template.Application.Abstractions;
using Template.Application.Abstractions.Interface;
using Template.Application.Repositories;
using Template.Common.Domain;
using Template.Common.Domain.Errors;
using Template.Domain.Users;

namespace Template.Application.Feature.Users.Commands.Delete;

public class DeleteUserCommandHandler : CommandHandlerBase<DeleteUserCommand, Guid>
{
    private readonly IUserRepository _repo;
    private User? _delete;

    public DeleteUserCommandHandler(
        IUserRepository repo,
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher domainEventDispatcher)
        : base(domainEventDispatcher, unitOfWork)
    {
        _repo = repo;
    }

    protected override async Task<Result<Guid, IDomainError>> ExecuteAsync(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Core operation
        if (await _repo.GetByIdAsync(request.Id) == null)
            return Result.Failure<Guid, IDomainError>(DomainError.Conflict($"No record found for ID: {request.Id}"));

        _delete = User.Delete(request.firstName, request.lastName, request.gender, request.email, request.phone);

        await _repo.DeleteAsync(_delete.Id);

        return Result.Success<Guid, IDomainError>(_delete.Id);
    }

    protected override IAggregateRoot? GetAggregateRoot(Result<Guid, IDomainError> result)
    {
        // Return the created aggregate root to dispatch domain events
        return _delete;
    }
}
