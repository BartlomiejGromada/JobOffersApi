using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Core.Queries.Handlers;

internal sealed class GetJobOfferQueryHandler : IQueryHandler<GetJobOfferQuery, JobOfferDetailsDto?>
{
    private readonly IJobOffersStorage _storage;

    public GetJobOfferQueryHandler(IJobOffersStorage storage)
    {
        _storage = storage;
    }

    public Task<JobOfferDetailsDto?> HandleAsync(GetJobOfferQuery query, CancellationToken cancellationToken = default)
        => _storage.GetDetailsAsync(query.Id, cancellationToken);
}
