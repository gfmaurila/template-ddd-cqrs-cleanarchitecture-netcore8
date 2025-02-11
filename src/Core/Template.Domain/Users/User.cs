using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Common.Domain;
using Template.Common.Domain.Enumerado;
using Template.Domain.Users.Events;

namespace Template.Domain.Users;

public sealed class User : AggregateRoot<User>
{
    /// <summary>
    /// Gets the first name of the entity.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets the last name of the entity.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Gets the gender of the entity.
    /// </summary>
    public EGender Gender { get; private set; }

    /// <summary>
    /// Gets the email of the entity.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the phone number of the entity.
    /// </summary>
    public string Phone { get; private set; }

    private User(string firstName, string lastName, EGender gender, string email, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        Phone = phone;
    }

    public static User Create(string firstName, string lastName, EGender gender, string email, string phone)
    {
        var obj = new User(firstName, lastName, gender, email, phone);
        obj.RaiseDomainEvent(new UserCreatedEvent(obj.Id, obj.FirstName, obj.LastName, obj.Gender, obj.Email, obj.Phone));
        return obj;
    }

    public static User Update(string firstName, string lastName, EGender gender, string email, string phone)
    {
        var obj = new User(firstName, lastName, gender, email, phone);
        obj.RaiseDomainEvent(new UserUpdateEvent(obj.Id, obj.FirstName, obj.LastName, obj.Gender, obj.Email, obj.Phone));
        return obj;
    }

    public static User Delete(string firstName, string lastName, EGender gender, string email, string phone)
    {
        var obj = new User(firstName, lastName, gender, email, phone);
        obj.RaiseDomainEvent(new UserDeletedEvent(obj.Id, obj.FirstName, obj.LastName, obj.Gender, obj.Email, obj.Phone));
        return obj;
    }
}
