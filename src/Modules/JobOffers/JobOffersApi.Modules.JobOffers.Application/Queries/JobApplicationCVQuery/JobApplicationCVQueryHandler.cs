using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.Services;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationCVQuery;

internal sealed class JobApplicationCVQueryHandler : IQueryHandler<JobApplicationCVQuery, byte[]?>
{
    private readonly IJobApplicationsStorage _jobApplicationStorage;
    private readonly IAuthorizationJobApplicationService _authorizationJobApplicationService;

    public JobApplicationCVQueryHandler(
        IJobApplicationsStorage jobApplicationStorage,
        IAuthorizationJobApplicationService authorizationJobApplicationService)
    {
        _jobApplicationStorage = jobApplicationStorage;
        _authorizationJobApplicationService = authorizationJobApplicationService;
    }

    public async Task<byte[]?> HandleAsync(JobApplicationCVQuery query, CancellationToken cancellationToken = default)
    {
        await _authorizationJobApplicationService.ValidateAccessToJobApplication(
            query.JobOfferId, query.JobApplicationId, cancellationToken);

        var jobApplication = await _jobApplicationStorage.GetAsync(
             query.JobOfferId, query.JobApplicationId, cancellationToken);

        return await _jobApplicationStorage.GetCVAsync(query.JobOfferId, query.JobApplicationId, cancellationToken);
    }
}
