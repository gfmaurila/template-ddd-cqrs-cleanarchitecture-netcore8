using AutoMapper;
using Template.Domain.Users;
using Template.Infrastructure.Persistence.Entities;

namespace Template.Infrastructure.Persistence.Profiles;


public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Domain -> Entity mappings
        CreateMap<User, UserEntity>();

        // Entity -> Domain mappings
        CreateMap<UserEntity, User>();
    }
}