using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class JobOfferNotFoundException : NotFoundException
{
    public JobOfferNotFoundException(Guid id)
        : base($"Job offer with id: {id} not found")
    {
    }
}
