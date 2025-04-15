using JobOffersApi.Modules.Companies.Core.Entities;

namespace JobOffersApi.Modules.Companies.Core.Repositories;

internal interface ICompaniesRepository
{
    Task<Company?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Company company, CancellationToken cancellationToken = default);
    Task AddAsync(Company company, CancellationToken cancellationToken = default);
    Task RemoveAsync(Company company, CancellationToken cancellationToken = default);
}
