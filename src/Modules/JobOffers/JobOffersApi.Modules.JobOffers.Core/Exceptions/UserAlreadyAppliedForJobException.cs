using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class UserAlreadyAppliedForJobException : ModularException
{
    public UserAlreadyAppliedForJobException(Guid userId, Guid jobOfferId) 
        : base($"User with id: {userId} already applied for job with id: {jobOfferId}")
    {
    }
}
