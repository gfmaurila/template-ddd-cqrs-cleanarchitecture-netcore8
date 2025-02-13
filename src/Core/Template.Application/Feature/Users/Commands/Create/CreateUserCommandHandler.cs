using CSharpFunctionalExtensions;
using Template.Application.Abstractions;
using Template.Application.Abstractions.Interface;
using Template.Application.Repositories;
using Template.Common.Domain;
using Template.Common.Domain.Errors;
using Template.Domain.Users;

namespace Template.Application.Feature.Users.Commands.Create;

public class CreateUserCommandHandler : CommandHandlerBase<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repo;
    private User? _created;

    public CreateUserCommandHandler(
        IUserRepository repo,
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher domainEventDispatcher)
        : base(domainEventDispatcher, unitOfWork)
    {
        _repo = repo;
    }

    protected override async Task<Result<Guid, IDomainError>> ExecuteAsync(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Core operation
        if (await _repo.ExistsByEmailAsync(request.email))
            return Result.Failure<Guid, IDomainError>(DomainError.Conflict("User already exist"));

        _created = User.Create(request.firstName, request.lastName, request.gender, request.email, request.phone);

        await _repo.AddAsync(_created, cancellationToken);

        return Result.Success<Guid, IDomainError>(_created.Id);
    }

    protected override IAggregateRoot? GetAggregateRoot(Result<Guid, IDomainError> result)
    {
        // Return the created aggregate root to dispatch domain events
        return _created;
    }
}
