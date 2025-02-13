using Template.Common.Domain;
using Template.Common.Domain.Enumerado;

namespace Template.Infrastructure.Persistence.Entities;

public class UserEntity : BaseEntity
{
    /// <summary>
    /// Gets the first name of the entity.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets the last name of the entity.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets the gender of the entity.
    /// </summary>
    public EGender Gender { get; set; }

    /// <summary>
    /// Gets the email of the entity.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets the phone number of the entity.
    /// </summary>
    public string Phone { get; set; }
}
