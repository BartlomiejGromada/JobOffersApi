using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Services;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Services;

internal sealed class AuthorizationJobApplicationService : IAuthorizationJobApplicationService
{
    private readonly IJobApplicationsStorage _jobApplicationStorage;
    private readonly IJobOffersStorage _jobOffersStorage;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IContext _context;

    public AuthorizationJobApplicationService(
        IJobApplicationsStorage jobApplicationStorage,
        IJobOffersStorage jobOffersStorage,
        IAuthorizationCompanyService authorizationCompanyService,
        IContext context)
    {
        _jobApplicationStorage = jobApplicationStorage;
        _jobOffersStorage = jobOffersStorage;
        _authorizationCompanyService = authorizationCompanyService;
        _context = context;
    }

    public async Task ValidateAccessToJobApplication(
        Guid jobOfferId, Guid jobApplicationId, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;

        var jobOffer = await _jobOffersStorage.GetAsync(jobOfferId, cancellationToken);

        if (jobOffer == null)
        {
            throw new JobOfferNotFoundException(jobOfferId);
        }

        var jobApplication = await _jobApplicationStorage.GetAsync(
             jobOfferId, jobApplicationId, cancellationToken);

        if(jobApplication == null)
        {
            throw new JobApplicationNotFoundException(jobOfferId);
        }

        if(identity.Role == Roles.Admin)
        {
            return;
        }

        if (identity.Role == Roles.Candidate && jobApplication?.CandidateId != identity.Id)
        {
            throw new InvalidAccessToJobApplicationException(jobApplicationId, identity.Id);
        }

        if (identity.Role != Roles.Candidate)
        {
            await _authorizationCompanyService.ValidateWorkingInCompanyAsync(identity.Id,
                    jobOffer.CompanyId, cancellationToken);
        }
    }
}
