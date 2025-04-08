using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Core.Exceptions
{
    internal class EmailInUseException : ModularException
    {
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}