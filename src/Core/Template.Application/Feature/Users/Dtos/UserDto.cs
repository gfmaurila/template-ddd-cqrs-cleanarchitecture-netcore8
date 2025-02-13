using Template.Common.Domain.Enumerado;

namespace Template.Application.Feature.Users.Dtos;

public sealed class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public EGender Gender { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
}
