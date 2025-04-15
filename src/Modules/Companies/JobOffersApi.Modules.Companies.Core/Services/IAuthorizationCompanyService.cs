namespace JobOffersApi.Modules.Companies.Core.Services;

internal interface IAuthorizationCompanyService
{
    Task<bool> IsWorkingInCompanyAsync(Guid userId, Guid companyId, CancellationToken cancellationToken = default);
    Task<bool> IsCompanyOwnerAsync(Guid userId, Guid companyId, CancellationToken cancellationToken = default);
}
