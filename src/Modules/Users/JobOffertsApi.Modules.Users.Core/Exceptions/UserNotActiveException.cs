using System;
using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Core.Exceptions
{
    internal class UserNotActiveException : ModularException
    {
        public Guid UserId { get; }

        public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
        {
            UserId = userId;
        }
    }
}