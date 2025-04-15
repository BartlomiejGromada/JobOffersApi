using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Exceptions;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Storages;

namespace JobOffersApi.Modules.JobOffers.Application.Services;

internal sealed class JobOffersService : IJobOffersService
{
    private readonly IJobOffersStorage _jobOffersStorage;
    private readonly ICompaniesService _companiesService;

    public JobOffersService(IJobOffersStorage jobOffersStorage, ICompaniesService companiesService)
    {
        _jobOffersStorage = jobOffersStorage;
        _companiesService = companiesService;
    }

    public async Task ValidateAccessAsync(
        Guid jobOfferId, 
        Guid userId,
        string userRole,
        CancellationToken cancellationToken = default)
    {
        var jobOffer = await _jobOffersStorage.GetAsync(jobOfferId, cancellationToken);
        if (jobOffer is null)
        {
            throw new JobOfferNotFoundException(jobOfferId);
        }

        if(userRole == Roles.Admin)
        {
            return;
        }

        if(userRole == Roles.Employer || userRole == Roles.OwnerCompany)
        {
            var hasAccess = await _companiesService.HasAccessAsync(jobOffer.CompanyId, userId, cancellationToken);
            if (!hasAccess)
            {
                throw new UnauthorizedCompanyAccessException(jobOffer.CompanyId, userId);
            }
            return;
        }
    }
}
