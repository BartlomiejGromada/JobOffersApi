using System;
using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Core.Exceptions
{
    internal class UserStateCannotBeChangedException : ModularException
    {
        public string State { get; }
        public Guid UserId { get; }

        public UserStateCannotBeChangedException(string state, Guid userId)
            : base($"User state cannot be changed to: '{state}' for user with ID: '{userId}'.")
        {
            State = state;
            UserId = userId;
        }
    }
}