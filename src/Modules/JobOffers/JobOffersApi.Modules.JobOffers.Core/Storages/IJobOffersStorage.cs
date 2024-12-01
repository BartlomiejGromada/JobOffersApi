using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Queries;

namespace JobOffersApi.Modules.JobOffers.Core.Storages;

internal interface IJobOffersStorage
{
    public Task<Paged<JobOfferDto>> GetPagedAsync(JobOffersQuery query, CancellationToken cancellationToken = default);
    public Task<JobOfferDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default);
}
