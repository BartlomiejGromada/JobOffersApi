namespace JobOffersApi.Modules.Companies.Integration.Services;

public interface IAuthorizationCompanyService
{
    Task ValidateWorkingInCompanyAsync(Guid userId, Guid companyId, CancellationToken cancellationToken = default);
    Task ValidateCompanyOwnerAsync(Guid userId, Guid companyId, CancellationToken cancellationToken = default);
}
