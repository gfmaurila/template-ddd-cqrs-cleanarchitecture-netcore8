using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Common.Domain;
using Template.Common.Domain.Enumerado;

namespace Template.Domain.Users.Events;

public sealed class UserUpdateEvent : BaseUserDomainEvent
{
    public UserUpdateEvent(Id<User> id, string firstName, string lastName, EGender gender, string email, string phone)
        : base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        Phone = phone;
        AggregateId = Guid.NewGuid();
        
        //MessageType = nameof(ExempleBaseEvent);
    }

    /// <summary>
    /// Gets the first name of the Exemple entity.
    /// </summary>
    public string FirstName { get; private init; }

    /// <summary>
    /// Gets the last name of the Exemple entity.
    /// </summary>
    public string LastName { get; private init; }

    /// <summary>
    /// Gets the gender of the Exemple entity.
    /// </summary>
    public EGender Gender { get; private init; }

    /// <summary>
    /// Gets the email address of the Exemple entity.
    /// </summary>
    public string Email { get; private init; }

    /// <summary>
    /// Gets the phone number of the Exemple entity.
    /// </summary>
    public string Phone { get; private init; }

    
}
