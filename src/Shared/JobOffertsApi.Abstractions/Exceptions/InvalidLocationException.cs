using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class InvalidLocationException : ModularException
{
    public InvalidLocationException(string message) : base(message)
    {
    }
}
