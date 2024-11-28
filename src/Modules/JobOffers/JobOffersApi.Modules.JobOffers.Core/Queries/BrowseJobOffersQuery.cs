using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO;
using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.JobOffers.Core.Queries;

internal class BrowseJobOffersQuery : PagedQuery<JobOfferDto>
{
    public string? Title { get; init; }
    public DateTimeOffset? CreatedFrom { get; init; }
    public DateTimeOffset? CreatedTo { get; init; }
    public string? CompanyName { get; init; }
    public List<JobAttribute> JobAttributes { get; init; } = new();
}
