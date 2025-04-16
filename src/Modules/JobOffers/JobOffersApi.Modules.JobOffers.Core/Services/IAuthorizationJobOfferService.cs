namespace JobOffersApi.Modules.JobOffers.Core.Services;

internal interface IAuthorizationJobOfferService
{
    Task ValidateAccessToJobOffer(Guid jobOfferId, CancellationToken cancellationToken = default);
}
