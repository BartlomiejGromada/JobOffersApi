using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.Handlers;

internal sealed class JobOfferQueryHandler : IQueryHandler<JobOfferQuery, JobOfferDetailsDto?>
{
    private readonly IJobOffersStorage _storage;

    public JobOfferQueryHandler(IJobOffersStorage storage)
    {
        _storage = storage;
    }

    public Task<JobOfferDetailsDto?> HandleAsync(JobOfferQuery query, CancellationToken cancellationToken = default)
        => _storage.GetDetailsAsync(query.Id, cancellationToken);
}
