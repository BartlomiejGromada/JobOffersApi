using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Services;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationsQuery;

internal sealed class JobApplicationsQueryHandler : IQueryHandler<JobApplicationsQuery, Paged<JobApplicationDto>>
{
    private readonly IJobApplicationsStorage _jobApplicationStorage;
    private readonly IJobOffersStorage _jobOffersStorage;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IAuthorizationJobApplicationService _authorizationJobApplicationService;
    private readonly IContext _context;

    public JobApplicationsQueryHandler(
        IJobApplicationsStorage jobApplicationStorage,
        IJobOffersStorage jobOffersStorage,
        IAuthorizationCompanyService authorizationCompanyService,
        IAuthorizationJobApplicationService authorizationJobApplicationService,
        IContext context)
    {
        _jobApplicationStorage = jobApplicationStorage;
        _jobOffersStorage = jobOffersStorage;
        _authorizationCompanyService = authorizationCompanyService;
        _authorizationJobApplicationService = authorizationJobApplicationService;
        _context = context;
    }

    public async Task<Paged<JobApplicationDto>> HandleAsync(JobApplicationsQuery query, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;
        var jobOffer = await _jobOffersStorage.GetAsync(query.JobOfferId, cancellationToken);

        if (jobOffer == null)
        {
            throw new JobOfferNotFoundException(query.JobOfferId);
        }

        if (identity.Role == Roles.Employer || identity.Role == Roles.CompanyOwner)
        {
            await _authorizationCompanyService.ValidateWorkingInCompanyAsync(identity.Id,
                    jobOffer.CompanyId, cancellationToken);
        }

        return await _jobApplicationStorage.GetPagedAsync(
            query.JobOfferId,
            query.Page,
            query.Results,
            cancellationToken);
    }
}
