using AutoMapper;
using CSharpFunctionalExtensions;
using Template.Application.Abstractions;
using Template.Application.Abstractions.Interface;
using Template.Application.Feature.Users.Dtos;
using Template.Application.Repositories;
using Template.Common.Domain;
using Template.Common.Domain.Errors;
using Template.Domain.Users;

namespace Template.Application.Feature.Users.Commands.Delete;

public class DeleteUserCommandHandler : CommandHandlerBase<DeleteUserCommand, Guid>
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    private User? _delete;

    public DeleteUserCommandHandler(
        IUserRepository repo,
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher domainEventDispatcher,
        IMapper mapper)
        : base(domainEventDispatcher, unitOfWork)
    {
        _repo = repo;
        _mapper = mapper;
    }

    protected override async Task<Result<Guid, IDomainError>> ExecuteAsync(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Core operation
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null)
            return Result.Failure<Guid, IDomainError>(DomainError.Conflict($"No record found for ID: {request.Id}"));

        var mapper = _mapper.Map<UserDto>(entity);

        _delete = User.Delete(mapper.FirstName, mapper.LastName, mapper.Gender, mapper.Email, mapper.Phone);

        await _repo.DeleteAsync(request.Id);

        return Result.Success<Guid, IDomainError>(_delete.Id);
    }

    protected override IAggregateRoot? GetAggregateRoot(Result<Guid, IDomainError> result)
    {
        // Return the created aggregate root to dispatch domain events
        return _delete;
    }
}
