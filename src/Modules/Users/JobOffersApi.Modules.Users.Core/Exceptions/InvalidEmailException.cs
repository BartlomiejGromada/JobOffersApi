using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Core.Exceptions;

internal class InvalidEmailException : ModularException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Invalid email: {email}.")
    {
        Email = email;
    }
}