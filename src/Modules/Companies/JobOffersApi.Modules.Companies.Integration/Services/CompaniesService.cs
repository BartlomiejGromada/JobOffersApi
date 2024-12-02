using JobOffersApi.Modules.Companies.Core.Storages;

namespace JobOffersApi.Modules.Companies.Integration.Services;

internal sealed class CompaniesService : ICompaniesService
{
    private readonly ICompaniesStorage _companiesStorage;

    public CompaniesService(ICompaniesStorage companiesStorage)
    {
        _companiesStorage = companiesStorage;
    }

    public Task<bool> HasAccessAsync(Guid companyId, Guid employerId, CancellationToken cancellationToken = default)
        => _companiesStorage.IsWorkingAsync(companyId, employerId, cancellationToken);
}
