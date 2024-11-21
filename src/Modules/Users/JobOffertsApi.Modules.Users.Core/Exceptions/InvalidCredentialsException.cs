using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Core.Exceptions;

internal class InvalidCredentialsException : ModularException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}