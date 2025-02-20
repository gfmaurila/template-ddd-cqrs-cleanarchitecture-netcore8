using AutoMapper;
using Template.Application.Feature.Users.Dtos;
using Template.Domain.Users;

namespace Template.Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Map User to UserDto
        CreateMap<User, UserDto>();

        // Map UserDto to User
        CreateMap<UserDto, User>();
    }
}
