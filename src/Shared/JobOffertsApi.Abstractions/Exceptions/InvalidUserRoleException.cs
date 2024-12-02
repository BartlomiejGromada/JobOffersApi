namespace JobOffersApi.Abstractions.Exceptions;

public class InvalidUserRoleException : ModularException
{
    public InvalidUserRoleException(string message) : base(message)
    {
    }
}
