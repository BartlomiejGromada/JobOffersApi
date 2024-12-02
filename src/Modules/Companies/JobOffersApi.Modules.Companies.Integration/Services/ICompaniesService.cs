namespace JobOffersApi.Modules.Companies.Integration.Services;

public interface ICompaniesService
{
    Task<bool> HasAccessAsync(Guid companyId, Guid employerId, CancellationToken cancellationToken = default);
}
