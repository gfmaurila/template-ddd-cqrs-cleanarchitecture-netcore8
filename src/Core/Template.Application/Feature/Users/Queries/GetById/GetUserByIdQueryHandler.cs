using AutoMapper;
using CSharpFunctionalExtensions;
using Template.Application.Abstractions.Interface;
using Template.Application.Feature.Users.Dtos;
using Template.Application.Repositories;
using Template.Common.Domain.Errors;

namespace Template.Application.Feature.Users.Queries.GetById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    public GetUserByIdQueryHandler(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Result<UserDto, IDomainError>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity is null)
            return Result.Failure<UserDto, IDomainError>(DomainError.NotFound("User not found."));

        // Map the User domain object to a UserDto
        return Result.Success<UserDto, IDomainError>(_mapper.Map<UserDto>(entity));

    }
}
