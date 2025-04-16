using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Services;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationQuery;

internal class JobApplicationQueryHandler : IQueryHandler<JobApplicationQuery, JobApplicationDto?>
{
    private readonly IJobApplicationsStorage _jobApplicationStorage;
    private readonly IAuthorizationJobApplicationService _authorizationJobApplicationService;

    public JobApplicationQueryHandler(
        IJobApplicationsStorage jobApplicationStorage,
        IJobOffersStorage jobOffersStorage,
        IAuthorizationJobApplicationService authorizationJobApplicationService)
    {
        _jobApplicationStorage = jobApplicationStorage;
        _authorizationJobApplicationService = authorizationJobApplicationService;
    }

    public async Task<JobApplicationDto?> HandleAsync(JobApplicationQuery query, CancellationToken cancellationToken = default)
    {
        await _authorizationJobApplicationService.ValidateAccessToJobApplication(
            query.JobOfferId, query.JobApplicationId, cancellationToken);

        var jobApplication = await _jobApplicationStorage.GetAsync(
             query.JobOfferId, query.JobApplicationId, cancellationToken);

        return jobApplication;
    }
}