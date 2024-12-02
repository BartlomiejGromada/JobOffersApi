using JobOffersApi.Modules.Companies.Core.Entities;

namespace JobOffersApi.Modules.Companies.Core.Repositories;

internal interface IEmployersRepository
{
    Task<Employer?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Employer employer, CancellationToken cancellationToken = default);
}
