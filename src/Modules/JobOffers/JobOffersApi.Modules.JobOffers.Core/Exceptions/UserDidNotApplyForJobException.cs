using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class UserDidNotApplyForJobException : ModularException
{
    public UserDidNotApplyForJobException(Guid userId, Guid jobOfferId) 
        : base($"User with id: {userId} did not apply for job with id: {jobOfferId}")
    {
    }
}
