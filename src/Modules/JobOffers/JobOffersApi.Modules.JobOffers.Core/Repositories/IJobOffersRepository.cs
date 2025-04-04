using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.JobOffers.Core.Repositories;

internal interface IJobOffersRepository
{
    public Task<JobOffer?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    public Task AddAsync(JobOffer jobOffer, CancellationToken cancellationToken = default);
    public Task UpdateAsync(JobOffer jobOffer, CancellationToken cancellationToken = default);
    public Task RemoveAsync(JobOffer jobOffer, CancellationToken cancellationToken = default);
}
