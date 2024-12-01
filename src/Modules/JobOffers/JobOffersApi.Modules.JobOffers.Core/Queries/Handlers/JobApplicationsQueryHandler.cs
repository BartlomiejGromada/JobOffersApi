using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Core.Queries.Handlers;

internal class JobApplicationsQueryHandler : IQueryHandler<JobApplicationsQuery, Paged<JobApplicationDto>>
{
    private readonly IJobApplicationsStorage _storage;

    public JobApplicationsQueryHandler(IJobApplicationsStorage storage)
    {
        _storage = storage;
    }

    public async Task<Paged<JobApplicationDto>> HandleAsync(JobApplicationsQuery query, CancellationToken cancellationToken = default)
    {
        //TODO: Validation after added Companies module

        return await _storage.GetPagedAsync(query, cancellationToken);
    }
}
