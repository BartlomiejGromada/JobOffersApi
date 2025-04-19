using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobOffersQuery;

internal class JobOffersQuery : PagedQuery<JobOfferDto>
{
    public string? Title { get; init; }
    public DateTimeOffset? CreatedFrom { get; init; }
    public DateTimeOffset? CreatedTo { get; init; }
    public string? CompanyName { get; init; }
    public string? City { get; init; }
    public bool? OnlyUnexpiredOffers { get; init; }
}