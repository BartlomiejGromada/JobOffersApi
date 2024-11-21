using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class InvalidAvailabilityDateException : ModularException
{
    public InvalidAvailabilityDateException(DateOnly date) : base($"Invalid availability date: {date:dd-MM-yyyy}")
    {
    }
}
