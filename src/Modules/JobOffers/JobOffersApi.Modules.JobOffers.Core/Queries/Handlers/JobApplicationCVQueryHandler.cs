using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Core.Queries.Handlers;

internal sealed class JobApplicationCVQueryHandler : IQueryHandler<JobApplicationCVQuery, byte[]?>
{
    private readonly IJobApplicationsStorage _storage;

    public JobApplicationCVQueryHandler(IJobApplicationsStorage storage)
    {
        _storage = storage;
    }

    public async Task<byte[]?> HandleAsync(JobApplicationCVQuery query, CancellationToken cancellationToken = default)
    {
        //TODO: Validation after added Companies module

        var cv = await _storage.GetCVAsync(query.JobOfferId, query.JobApplicationId, cancellationToken);

        return cv;
    }
}
