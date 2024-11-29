using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Modules.JobOffers.Core.DTO;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Entities.Extensions;
using JobOffersApi.Modules.JobOffers.Core.Queries;
using JobOffersApi.Modules.JobOffers.Core.Storages;
using JobOffersApi.Modules.Users.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.JobOffers.Infrastructure.Storages;

internal class JobOffersStorage : IJobOffersStorage
{
    private readonly IQueryable<JobOffer> _jobOffers;

    public JobOffersStorage(JobOffersDbContext dbContext)
    {
        _jobOffers = dbContext.Set<JobOffer>().AsNoTracking();
    }

    public async Task<Paged<JobOfferDto>> GetPagedAsync(BrowseJobOffersQuery query, CancellationToken cancellationToken = default)
    {
        var jobOffers = _jobOffers;

        if(!string.IsNullOrEmpty(query.Title)) 
        {
            jobOffers = jobOffers.Where(jo => jo.Title.Equals(query.Title));
        }

        if (query.CreatedFrom.HasValue)
        {
            jobOffers = jobOffers.Where(jo => jo.CreatedAt.Date >= query.CreatedFrom.Value);
        }

        if (query.CreatedTo.HasValue)
        {
            jobOffers = jobOffers.Where(jo => jo.CreatedAt.Date <= query.CreatedTo.Value);
        }

        if (!string.IsNullOrEmpty(query.CompanyName))
        {
            jobOffers = jobOffers.Where(jo => jo.CompanyName.Equals(query.CompanyName));
        }

        if (query.JobAttributes.Any())
        {
            jobOffers = jobOffers.Where(jo => jo.JobAttributes.Any(ja => query.JobAttributes.Contains(ja)));
        }

        return await jobOffers
            .OrderByDescending(jo => jo.CreatedAt)
            .Select(jo => jo.AsDto())
            .PaginateAsync(query, cancellationToken);
    }

    public async Task<JobOfferDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var jobOffer = await _jobOffers.SingleOrDefaultAsync(jo => jo.Id == id, cancellationToken);

        return jobOffer?.AsDetailsDto();
    }
}
