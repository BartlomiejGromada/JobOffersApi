using System;
using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Integration.Exceptions;

public class UserNotActiveException : ModularException
{
    public Guid UserId { get; }

    public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
    {
        UserId = userId;
    }
}