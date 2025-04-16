using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationsQuery;

internal class JobApplicationsQuery : PagedQuery<JobApplicationDto>
{
    public JobApplicationsQuery(
        Guid jobOfferId,
        BrowseApplicationsQuery browseQuery)
    {
        JobOfferId = jobOfferId;
        BrowseQuery = browseQuery;
    }

    public Guid JobOfferId { get; init; }
    public BrowseApplicationsQuery BrowseQuery { get; init; }
}

internal class BrowseApplicationsQuery : PagedQuery<JobApplicationDto>
{
}
