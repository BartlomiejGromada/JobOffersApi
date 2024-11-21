using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Users.Core.Exceptions
{
    internal class RoleNotFoundException : ModularException
    {
        public RoleNotFoundException(string role) : base($"Role: '{role}' was not found.")
        {
        }
    }
}