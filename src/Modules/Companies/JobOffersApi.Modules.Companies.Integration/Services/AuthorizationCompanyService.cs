using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Exceptions;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using JobOffersApi.Modules.Companies.Core.Storages;
using JobOffersApi.Modules.Users.Integration.Queries;

namespace JobOffersApi.Modules.Companies.Integration.Services;

internal sealed class AuthorizationCompanyService : IAuthorizationCompanyService
{
    private readonly ICompaniesStorage _companiesStorage;
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public AuthorizationCompanyService(
        ICompaniesStorage companiesStorage,
        IDispatcher dispatcher,
        IContext context)
    {
        _companiesStorage = companiesStorage;
        _dispatcher = dispatcher;
        _context = context;
    }

    public async Task ValidateWorkingInCompanyAsync(Guid userId, Guid companyId, CancellationToken cancellationToken = default)
    {
        if (_context.Identity.Role == Roles.Admin)
        {
            return;
        }

        var company = await _companiesStorage.GetAsync(companyId, cancellationToken);

        if (company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var isWorkingInCompany = company.Employers.Any(e => e.Id == userId);

        if(!isWorkingInCompany)
        {
            throw new EmployeeNotBelongToCompanyException(userId, companyId);
        }
    }

    public async Task ValidateCompanyOwnerAsync(Guid userId, Guid companyId, CancellationToken cancellationToken = default)
    {
        if (_context.Identity.Role == Roles.Admin)
        {
            return;
        }

        await ValidateWorkingInCompanyAsync(userId, companyId, cancellationToken);

        var user = await _dispatcher.QueryAsync(new UserQuery() { UserId = userId }, cancellationToken);

        var isOwnerCompany = user!.RoleName == Roles.CompanyOwner;

        if(!isOwnerCompany)
        {
            throw new NotCompanyOwnerException(userId);
        }
    }
}
