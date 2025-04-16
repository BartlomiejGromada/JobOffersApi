using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Application.Services;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationQuery;

internal class JobApplicationQueryHandler : IQueryHandler<JobApplicationQuery, JobApplicationDto?>
{
    private readonly IJobApplicationsStorage _storage;
    private readonly IJobOffersService _service;

    public JobApplicationQueryHandler(IJobApplicationsStorage storage, IJobOffersService service)
    {
        _storage = storage;
        _service = service;
    }

    public async Task<JobApplicationDto?> HandleAsync(JobApplicationQuery query, CancellationToken cancellationToken = default)
    {
        await _service.ValidateAccessAsync(query.JobOfferId, query.InvokerId, query.InvokerRole, cancellationToken);

        var jobApplication = await _storage.GetAsync(query.JobOfferId, query.JobApplicationId, cancellationToken);

        if (jobApplication?.CandidateId != query.InvokerId)
        {
            throw new InvalidAccessToJobApplicationException(query.JobApplicationId, query.InvokerId);
        }

        return jobApplication;
    }
}
