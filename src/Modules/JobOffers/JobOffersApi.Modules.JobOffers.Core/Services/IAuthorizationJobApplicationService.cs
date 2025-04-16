namespace JobOffersApi.Modules.JobOffers.Core.Services;

internal interface IAuthorizationJobApplicationService
{
    Task ValidateAccessToJobApplication(Guid jobOfferId, Guid jobApplicationId, CancellationToken cancellationToken = default);
}
