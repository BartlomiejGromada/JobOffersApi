using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Core.Exceptions
{
    internal class SignUpDisabledException : ModularException
    {
        public SignUpDisabledException() : base("Sign up is disabled.")
        {
        }
    }
}