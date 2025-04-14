using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Application.Services;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.Handlers;

internal sealed class JobApplicationCVQueryHandler : IQueryHandler<JobApplicationCVQuery, byte[]?>
{
    private readonly IJobApplicationsStorage _storage;
    private readonly IJobOffersService _service;

    public JobApplicationCVQueryHandler(
        IJobApplicationsStorage storage,
        IJobOffersService service)
    {
        _storage = storage;
        _service = service;
    }

    public async Task<byte[]?> HandleAsync(JobApplicationCVQuery query, CancellationToken cancellationToken = default)
    {
        await _service.ValidateAccessAsync(query.JobOfferId, query.InvokerId, query.InvokerRole, cancellationToken);

        var jobApplication = await _storage.GetAsync(query.JobOfferId, query.JobApplicationId, cancellationToken);

        if (jobApplication?.CandidateId != query.InvokerId)
        {
            throw new InvalidAccessToJobApplicationException(query.JobApplicationId, query.InvokerId);
        }

        return await _storage.GetCVAsync(query.JobOfferId, query.JobApplicationId, cancellationToken);
    }
}
