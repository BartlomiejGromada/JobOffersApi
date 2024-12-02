namespace JobOffersApi.Modules.JobOffers.Core.Services;

internal interface IJobOffersService
{
    Task ValidateAccessAsync(
        Guid jobOfferId,
        Guid userId,
        string userRole,
        CancellationToken cancellationToken = default);
}
