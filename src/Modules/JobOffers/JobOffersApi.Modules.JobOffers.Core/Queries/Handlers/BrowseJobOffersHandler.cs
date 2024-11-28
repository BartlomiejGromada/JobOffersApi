using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Core.Queries;

internal class BrowseJobOffersQueryHandler : IQueryHandler<BrowseJobOffersQuery, Paged<JobOfferDto>>
{
    private readonly IJobOffersStorage _storage;

    public BrowseJobOffersQueryHandler(IJobOffersStorage jobOffersStorage)
    {
        _storage = jobOffersStorage;
    }

    public Task<Paged<JobOfferDto>> HandleAsync(BrowseJobOffersQuery query, CancellationToken cancellationToken = default)
        => _storage.GetPagedAsync(query, cancellationToken);
}
