using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class InvalidAvailabilityException : ModularException
{
    public InvalidAvailabilityException() : base("Invalid availability status")
    {
    }
}
