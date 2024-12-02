using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Companies.Core.Storages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace JobOffersApi.Modules.Companies.Core.Policies;

internal sealed class CompanyAccessHandler : AuthorizationHandler<CompanyAccessRequirement>
{
    private readonly ICompaniesStorage _storage;
    private readonly IDispatcher _dispatcher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyAccessHandler(
        ICompaniesStorage storage,
        IDispatcher dispatcher,
        IHttpContextAccessor httpContextAccessor)
    {
        _storage = storage;
        _dispatcher = dispatcher;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        CompanyAccessRequirement requirement)
    {
    }
}
