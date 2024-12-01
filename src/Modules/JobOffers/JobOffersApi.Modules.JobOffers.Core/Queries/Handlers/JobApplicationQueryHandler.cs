using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Core.Queries.Handlers;

internal class JobApplicationQueryHandler : IQueryHandler<JobApplicationQuery, JobApplicationDto?>
{
    private readonly IJobApplicationsStorage _storage;

    public JobApplicationQueryHandler(IJobApplicationsStorage storage)
    {
        _storage = storage;
    }

    public async Task<JobApplicationDto?> HandleAsync(JobApplicationQuery query, CancellationToken cancellationToken = default)
    {
        //TODO: Validation after added Companies module

        var dto = await _storage.GetAsync(query.JobOfferId, query.JobApplicationId, cancellationToken);

        return dto;
    }
}
