using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Queries;

namespace JobOffersApi.Modules.JobOffers.Core.Storages;

internal interface IJobApplicationsStorage
{
    public Task<Paged<JobApplicationDto>> GetPagedAsync(JobApplicationsQuery query, CancellationToken cancellationToken = default);
    public Task<JobApplicationDto?> GetAsync(Guid jobOfferId, Guid jobApplicationId, CancellationToken cancellationToken = default);
    public Task<byte[]?> GetCVAsync(Guid jobOfferId, Guid jobApplicationId, CancellationToken cancellationToken);
}
