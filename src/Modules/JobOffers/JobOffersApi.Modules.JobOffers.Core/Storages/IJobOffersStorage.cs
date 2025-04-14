using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.JobOffers.Core.Storages;

internal interface IJobOffersStorage
{
    public Task<Paged<JobOfferDto>> GetPagedAsync(
        string? title,
        DateTimeOffset? createdFrom,
        DateTimeOffset? createdTo,
        string? companyName,
        List<JobAttribute>? jobAttributes,
        string? city,
        bool? onlyUnexpiredOffers,
        int page,
        int results,
        CancellationToken cancellationToken = default);
    public Task<JobOfferDetailsDto?> GetDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default);
    public Task<JobOfferDto?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
