using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationsQuery;

internal class JobApplicationsQuery : PagedQuery<JobApplicationDto>
{
    public JobApplicationsQuery(
        Guid jobOfferId,
        Guid invokerId,
        string invokerRole,
        BrowseApplicationsQuery browseQuery)
    {
        JobOfferId = jobOfferId;
        InvokerId = invokerId;
        InvokerRole = invokerRole;
        BrowseQuery = browseQuery;
    }

    public Guid JobOfferId { get; init; }
    public Guid InvokerId { get; init; }
    public string InvokerRole { get; init; }
    public BrowseApplicationsQuery BrowseQuery { get; init; }
}

internal class BrowseApplicationsQuery : PagedQuery<JobApplicationDto>
{
}
