using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Application.Services;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationsQuery;

internal sealed class JobApplicationsQueryHandler : IQueryHandler<JobApplicationsQuery, Paged<JobApplicationDto>>
{
    private readonly IJobApplicationsStorage _storage;
    private readonly IJobOffersService _service;

    public JobApplicationsQueryHandler(IJobApplicationsStorage storage, IJobOffersService service)
    {
        _storage = storage;
        _service = service;
    }

    public async Task<Paged<JobApplicationDto>> HandleAsync(JobApplicationsQuery query, CancellationToken cancellationToken = default)
    {
        await _service.ValidateAccessAsync(
            query.JobOfferId,
            query.InvokerId,
            query.InvokerRole,
            cancellationToken);

        return await _storage.GetPagedAsync(
            query.JobOfferId,
            query.InvokerId,
            query.InvokerRole,
            query.Page,
            query.Results,
            cancellationToken);
    }
}
