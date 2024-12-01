using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Modules.JobOffers.Core.DTO.Extensions;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Queries;
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

    public async Task<Paged<JobOfferDto>> GetPagedAsync(JobOffersQuery query, CancellationToken cancellationToken = default)
    {
        var jobOffers = _jobOffers;

        if (query.OnlyUnexpiredOffers is true)
        {
            var today = _clock.CurrentDateOffset();
            jobOffers = jobOffers.Where(jo => jo.ExpirationDate >= today);
        }

        if (!string.IsNullOrEmpty(query.Title)) 
        {
            jobOffers = jobOffers.Where(jo => jo.Title.ToLower().Contains(query.Title.ToLower()));
        }

        if (query.CreatedFrom.HasValue)
        {
            jobOffers = jobOffers.Where(jo => jo.CreatedDate.Date >= query.CreatedFrom.Value);
        }

        if (query.CreatedTo.HasValue)
        {
            jobOffers = jobOffers.Where(jo => jo.CreatedDate.Date <= query.CreatedTo.Value);
        }

        if (!string.IsNullOrEmpty(query.CompanyName))
        {
            jobOffers = jobOffers.Where(jo => jo.CompanyName.ToLower().Contains(query.CompanyName.ToLower()));
        }

        if (query.JobAttributes.Any())
        {
            jobOffers = jobOffers.Where(jo => jo.JobAttributes.Any(ja => query.JobAttributes.Contains(ja)));
        }

        if (!string.IsNullOrEmpty(query.City))
        {
            jobOffers = jobOffers.Where(jo => jo.Location.City.ToLower().Equals(query.City.ToLower()));
        }

        return await jobOffers
            .OrderByDescending(jo => jo.CreatedDate)
            .Select(jo => jo.ToDto())
            .PaginateAsync(query, cancellationToken);
    }

    public async Task<JobOfferDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var jobOffer = await _jobOffers
            .Include(jo => jo.JobAttributes)
            .SingleOrDefaultAsync(jo => jo.Id == id, cancellationToken);

        return jobOffer?.ToDetailsDto();
    }
}
