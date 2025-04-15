
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using JobOffersApi.Modules.Companies.Core.Services;
using JobOffersApi.Modules.Companies.Core.Storages;
using JobOffersApi.Modules.Users.Integration.Queries;

namespace JobOffersApi.Modules.Companies.Application.Services;

internal sealed class AuthorizationCompanyService : IAuthorizationCompanyService
{
    private readonly ICompaniesStorage _companiesStorage;
    private readonly IDispatcher _dispatcher;

    public AuthorizationCompanyService(ICompaniesStorage companiesStorage, IDispatcher dispatcher)
    {
        _companiesStorage = companiesStorage;
        _dispatcher = dispatcher;
    }

    public async Task<bool> IsWorkingInCompanyAsync(Guid userId, Guid companyId, CancellationToken cancellationToken = default)
    {
        var company = await _companiesStorage.GetAsync(companyId, cancellationToken);

        if (company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var isWorkingInCompany = company.Employers.Any(e => e.Id == userId);

        return isWorkingInCompany;
    }

    public async Task<bool> IsCompanyOwnerAsync(Guid userId, Guid companyId, CancellationToken cancellationToken = default)
    {
        var isWorkingInCompany = await IsWorkingInCompanyAsync(userId, companyId, cancellationToken);

        if (!isWorkingInCompany)
        {
            throw new EmployeeNotBelongToCompanyException(userId, companyId);
        }

        var user = await _dispatcher.QueryAsync(new UserQuery() { UserId = userId }, cancellationToken);

        var isOwnerCompany = user!.RoleName != Roles.OwnerCompany;

        return isOwnerCompany;
    }
}
