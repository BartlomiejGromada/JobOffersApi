using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class JobApplicationNotFoundException : ModularException
{
    public JobApplicationNotFoundException(Guid id)
        : base($"Job application with id: {id} not found")
    {
    }
}
