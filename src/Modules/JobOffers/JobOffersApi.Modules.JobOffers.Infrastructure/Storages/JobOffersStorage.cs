using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Modules.JobOffers.Core.DTO.Extensions;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Storages;
using JobOffersApi.Modules.Users.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.JobOffers.Infrastructure.Storages;

internal class JobOffersStorage : IJobOffersStorage
{
    private readonly IQueryable<JobOffer> _jobOffers;
    private readonly IClock _clock;

    public JobOffersStorage(JobOffersDbContext dbContext, IClock clock)
    {
        _jobOffers = dbContext.Set<JobOffer>().AsNoTracking();
        _clock = clock;
    }

    public async Task<Paged<JobOfferDto>> GetPagedAsync(
        string? title,
        DateTimeOffset? createdFrom,
        DateTimeOffset? createdTo,
        string? companyName,
        string? city,
        bool? onlyUnexpiredOffers,
        int page,
        int results,
        CancellationToken cancellationToken = default)
    {
        var jobOffers = _jobOffers;

        if (onlyUnexpiredOffers is true)
        {
            var today = _clock.CurrentDateOffset();
            jobOffers = jobOffers.Where(jo => jo.ExpirationDate >= today);
        }

        if (!string.IsNullOrEmpty(title)) 
        {
            jobOffers = jobOffers.Where(jo => jo.Title.ToLower().Contains(title.ToLower()));
        }

        if (createdFrom.HasValue)
        {
            jobOffers = jobOffers.Where(jo => jo.CreatedDate.Date >= createdFrom.Value);
        }

        if (createdTo.HasValue)
        {
            jobOffers = jobOffers.Where(jo => jo.CreatedDate.Date <= createdTo.Value);
        }

        if (!string.IsNullOrEmpty(companyName))
        {
            jobOffers = jobOffers.Where(jo => jo.CompanyName.ToLower().Contains(companyName.ToLower()));
        }

        if (!string.IsNullOrEmpty(city))
        {
            jobOffers = jobOffers.Where(jo => jo.Location.City.ToLower().Equals(city.ToLower()));
        }

        return await jobOffers
            .OrderByDescending(jo => jo.CreatedDate)
            .Select(jo => jo.ToDto())
            .PaginateAsync(page, results, cancellationToken);
    }

    public async Task<JobOfferDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var jobOffer = await _jobOffers
            .Include(jo => jo.JobAttributes)
            .SingleOrDefaultAsync(jo => jo.Id == id, cancellationToken);

        return jobOffer?.ToDetailsDto();
    }

    public async Task<JobOfferDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var jobOffer = await _jobOffers.SingleOrDefaultAsync(jo => jo.Id == id, cancellationToken);

        return jobOffer?.ToDto();
    }
}
