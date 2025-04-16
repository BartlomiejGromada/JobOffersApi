using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Services;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Services;

internal sealed class AuthorizationJobOfferService : IAuthorizationJobOfferService
{
    private readonly IJobOffersStorage _jobOffersStorage;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IContext _context;

    public AuthorizationJobOfferService(
        IJobOffersStorage jobOffersStorage,
        IAuthorizationCompanyService authorizationCompanyService,
        IContext context)
    {
        _jobOffersStorage = jobOffersStorage;
        _authorizationCompanyService = authorizationCompanyService;
        _context = context;
    }

    public async Task ValidateAccessToJobOffer(Guid jobOfferId, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;

        var jobOffer = await _jobOffersStorage.GetAsync(jobOfferId, cancellationToken);

        if (jobOffer == null)
        {
            throw new JobOfferNotFoundException(jobOfferId);
        }

        if(identity.Role == Roles.Admin)
        {
            return;
        }

        await _authorizationCompanyService.ValidateWorkingInCompanyAsync(identity.Id, jobOffer.CompanyId, cancellationToken);
    }
}
