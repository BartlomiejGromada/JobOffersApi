using JobOffersApi.Modules.Companies.Core.DTO.Companies;

namespace JobOffersApi.Modules.Companies.Core.Storages;

internal interface ICompaniesStorage
{
    Task<bool> IsWorkingAsync(Guid companyId, Guid employerId, CancellationToken cancellationToken = default);
    Task<bool> IsExistAsync(string name, CancellationToken cancellationToken = default);
    Task<CompanyDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
