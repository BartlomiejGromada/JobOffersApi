using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

namespace JobOffersApi.Modules.JobOffers.Core.Queries;


internal class JobApplicationsQuery : PagedQuery<JobApplicationDto>
{
    public JobApplicationsQuery(Guid jobOfferId, Guid employerId, BrowseApplicationsQuery browseQuery)
    {
        JobOfferId = jobOfferId;
        EmployerId = employerId;
        BrowseQuery = browseQuery;
    }

    public Guid JobOfferId { get; init; }
    public Guid EmployerId { get; init; }
    public BrowseApplicationsQuery BrowseQuery { get; init; }
}

internal class BrowseApplicationsQuery : PagedQuery<JobApplicationDto>
{
}
