namespace JobOffersApi.Modules.JobOffers.Application.Services;

internal interface IJobOffersService
{
    Task ValidateAccessAsync(
        Guid jobOfferId,
        Guid userId,
        string userRole,
        CancellationToken cancellationToken = default);
}
