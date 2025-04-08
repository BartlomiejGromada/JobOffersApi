using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Users.Core.DomainEvents;
using JobOffersApi.Modules.Users.Core.Exceptions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobOffersApi.Modules.Users.Core.Entities;

internal sealed class User : AggregateRoot<Guid>
{
    private User()
    {
        // EF CORE needs it
    }

    public User(
        string email, 
        string hashedPassword,
        string firstName,
        string lastName,
        Role role,
        DateOnly dateOfBirth,
        DateTimeOffset createdAt)
    {
        Email = email;
        HashedPassword = hashedPassword;
        FirstName = firstName;
        LastName = lastName;
        State = UserState.Active;
        Role = role;
        DateOfBirth = dateOfBirth;
        CreatedAt = createdAt;

        RaiseDomainEvent(new UserCreatedEvent(Id)); //example of raising domain event
    }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public Role Role { get; private set; }
    public string RoleId { get; private set; }
    public UserState State { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    [NotMapped]
    public bool IsLocked => State == UserState.Locked;

    [NotMapped]
    public bool IsActive => !IsLocked;

    public void SetHashedPassword(string hashedPassword) => HashedPassword = hashedPassword;

    public bool HasRole(string roleName) => Role.Name == roleName;

    public void SetState(UserState state)
    {
        if (HasRole(Roles.Admin))
        {
            throw new UserStateCannotBeChangedException(state.ToString(), Id);
        }

        State = state;
    }
}