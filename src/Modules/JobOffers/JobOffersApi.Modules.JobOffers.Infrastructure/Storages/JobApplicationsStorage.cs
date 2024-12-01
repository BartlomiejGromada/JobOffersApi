using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Modules.JobOffers.Core.DTO.Extensions;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Queries;
using JobOffersApi.Modules.JobOffers.Core.Storages;
using JobOffersApi.Modules.Users.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.JobOffers.Infrastructure.Storages;

internal class JobApplicationsStorage : IJobApplicationsStorage
{
    private readonly IQueryable<JobOffer> _jobOffers;

    public JobApplicationsStorage(JobOffersDbContext dbContext)
    {
        _jobOffers = dbContext.Set<JobOffer>().AsNoTracking();
    }

    public async Task<Paged<JobApplicationDto>> GetPagedAsync(
        JobApplicationsQuery query,
        CancellationToken cancellationToken = default)
    {
        var jobApplications = await _jobOffers
            .Where(jo => jo.Id == query.JobOfferId)
            .SelectMany(jo => jo.JobApplications)
            .Select(ja => new
            {
                ja.CandidateId,
                ja.CandidateFirstName,
                ja.CandidateLastName,
                ja.MessageToEmployer,
                ja.Status,
                ja.Disposition,
                ja.FinancialExpectations,
                ja.PreferredContract,
                ja.CreatedAt
            })
            .OrderByDescending(ja => ja.CreatedAt)
            .PaginateAsync(query, cancellationToken);

        return jobApplications.Map(x => new JobApplicationDto
        {
            CandidateFirstName = x.CandidateFirstName,
            CandidateLastName = x.CandidateLastName,
            MessageToEmployer = x.MessageToEmployer,
            Disposition = x.Disposition.ToDto(),
            PreferredContract = x.PreferredContract,
            FinancialExpectations = x.FinancialExpectations?.ToDto(),
            Status = x.Status,
            CreatedAt = x.CreatedAt,
        });
    }

    public async Task<JobApplicationDto?> GetAsync(
        Guid jobOfferId,
        Guid jobApplicationId, 
        CancellationToken cancellationToken = default)
    {
        var jobApp = await _jobOffers
             .Where(jo => jo.Id == jobOfferId)
             .SelectMany(jo => jo.JobApplications)
             .Where(ja => ja.Id == jobApplicationId)
             .Select(ja => new
             {
                 ja.CandidateId,
                 ja.CandidateFirstName,
                 ja.CandidateLastName,
                 ja.MessageToEmployer,
                 ja.Status,
                 ja.Disposition,
                 ja.FinancialExpectations,
                 ja.PreferredContract,
                 ja.CreatedAt
             })
             .SingleOrDefaultAsync(cancellationToken);

        return jobApp is not null ?
            new JobApplicationDto
            {
                CandidateFirstName = jobApp.CandidateFirstName,
                CandidateLastName = jobApp.CandidateLastName,
                MessageToEmployer = jobApp.MessageToEmployer,
                Disposition = jobApp.Disposition.ToDto(),
                PreferredContract = jobApp.PreferredContract,
                FinancialExpectations = jobApp.FinancialExpectations?.ToDto(),
                Status = jobApp.Status,
                CreatedAt = jobApp.CreatedAt,
            } : null;
    }

    public Task<byte[]?> GetCVAsync(
        Guid jobOfferId, 
        Guid jobApplicationId,
        CancellationToken cancellationToken)
        => _jobOffers
             .Where(jo => jo.Id == jobOfferId)
             .SelectMany(jo => jo.JobApplications)
             .Where(ja => ja.Id == jobApplicationId)
             .Select(ja => ja.CV)
             .SingleOrDefaultAsync(cancellationToken);
}
