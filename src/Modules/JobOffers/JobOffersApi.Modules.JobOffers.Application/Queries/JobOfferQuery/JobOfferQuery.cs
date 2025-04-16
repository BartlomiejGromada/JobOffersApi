using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobOfferQuery;

internal class JobOfferQuery : IQuery<JobOfferDetailsDto?>
{
    public JobOfferQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
