using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class JobOfferExpiredException : ModularException
{
    public JobOfferExpiredException(DateTimeOffset endDate)
        : base($"Job offer expired at: {endDate:dd-MM-yyyy HH:mm}")
    {
    }
}
