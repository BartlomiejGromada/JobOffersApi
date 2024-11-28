using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO;

namespace JobOffersApi.Modules.JobOffers.Core.Queries;

internal class GetJobOfferQuery : IQuery<JobOfferDetailsDto?>
{
    public GetJobOfferQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
