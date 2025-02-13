using Template.Common.Core.Events;
using Template.Common.Domain;
using Template.Common.Domain.Enumerado;
using Template.Domain.Users;

namespace Template.Application.Feature.Users.Messaging.Events.Integration;

public sealed class UserCreatedIntegrationEvent : IntegrationEvent
{
    public UserCreatedIntegrationEvent(Id<User> id, string firstName, string lastName, EGender gender, string email, string phone)
        : base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        Phone = phone;
        AggregateId = Guid.NewGuid();
    }
    public UserCreatedIntegrationEvent() { }

    /// <summary>
    /// Gets the first name of the Exemple entity.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets the last name of the Exemple entity.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets the gender of the Exemple entity.
    /// </summary>
    public EGender Gender { get; set; }

    /// <summary>
    /// Gets the email address of the Exemple entity.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets the phone number of the Exemple entity.
    /// </summary>
    public string Phone { get; set; }
}
