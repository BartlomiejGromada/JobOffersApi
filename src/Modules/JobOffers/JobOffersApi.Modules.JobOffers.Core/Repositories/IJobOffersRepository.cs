using JobOffersApi.Modules.JobOffers.Core.Entities;
using System.Linq.Expressions;

namespace JobOffersApi.Modules.JobOffers.Core.Repositories;

internal interface IJobOffersRepository
{
    public Task<JobOffer?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<JobOffer?> GetWithJobApplicationsAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<List<JobOffer>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);
    public Task AddAsync(JobOffer jobOffer, CancellationToken cancellationToken = default);
    public Task UpdateAsync(JobOffer jobOffer, CancellationToken cancellationToken = default);
    public Task RemoveAsync(JobOffer jobOffer, CancellationToken cancellationToken = default);
    public Task RemoveAsync(List<JobOffer> jobOffers, CancellationToken cancellationToken = default);
}