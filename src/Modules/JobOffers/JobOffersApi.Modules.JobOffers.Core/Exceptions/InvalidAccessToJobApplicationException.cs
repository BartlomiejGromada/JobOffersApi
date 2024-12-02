using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class InvalidAccessToJobApplicationException : ModularException
{
    public InvalidAccessToJobApplicationException(Guid jobApplicationId, Guid userId) : base($"User with id: {userId}" +
        $"doesn't have access to job application with id: {jobApplicationId}")
    {
    }
}
