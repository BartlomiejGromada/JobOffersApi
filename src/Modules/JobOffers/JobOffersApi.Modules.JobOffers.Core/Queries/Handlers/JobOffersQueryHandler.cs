using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Core.Queries;

internal class JobOffersQueryHandler : IQueryHandler<JobOffersQuery, Paged<JobOfferDto>>
{
    private readonly IJobOffersStorage _storage;

    public JobOffersQueryHandler(IJobOffersStorage jobOffersStorage)
    {
        _storage = jobOffersStorage;
    }

    public Task<Paged<JobOfferDto>> HandleAsync(JobOffersQuery query, CancellationToken cancellationToken = default)
        => _storage.GetPagedAsync(query, cancellationToken);
}
