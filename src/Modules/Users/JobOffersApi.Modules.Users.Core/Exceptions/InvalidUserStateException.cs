using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Core.Exceptions
{
    internal class InvalidUserStateException : ModularException
    {
        public string State { get; }

        public InvalidUserStateException(string state) : base($"User state is invalid: '{state}'.")
        {
            State = state;
        }
    }
}