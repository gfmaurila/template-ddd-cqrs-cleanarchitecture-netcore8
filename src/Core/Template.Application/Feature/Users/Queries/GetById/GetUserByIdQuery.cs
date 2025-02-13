using Template.Application.Abstractions.Interface;
using Template.Application.Feature.Users.Dtos;

namespace Template.Application.Feature.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery<UserDto>;
