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
        var basket = await _repo.GetByIdAsync(request.Id);
        if (basket is null)
            return Result.Failure<UserDto, IDomainError>(DomainError.NotFound("User not found."));

        // Map the User domain object to a UserDto
        var basketDto = _mapper.Map<UserDto>(basket);
        return Result.Success<UserDto, IDomainError>(basketDto);

    }
}
