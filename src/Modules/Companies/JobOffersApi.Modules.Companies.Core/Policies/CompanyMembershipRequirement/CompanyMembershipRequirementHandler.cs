using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Modules.Companies.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace JobOffersApi.Modules.Companies.Core.Policies.CompanyMembershipRequirement;

internal sealed class CompanyMembershipRequirementHandler : AuthorizationHandler<CompanyMembershipRequirement>
{
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyMembershipRequirementHandler(
        IAuthorizationCompanyService authorizationCompanyService,
        IContext context,
        IHttpContextAccessor httpContextAccessor)
    {
        _authorizationCompanyService = authorizationCompanyService;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CompanyMembershipRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var identity = _context.Identity;

        if (!httpContext.Request.RouteValues.TryGetValue("companyId", out var companyIdValue) ||
          !Guid.TryParse(companyIdValue?.ToString(), out Guid companyId))
        {
            return;
        }

        var isWorkingInCompany = await _authorizationCompanyService.IsWorkingInCompanyAsync(identity.Id, companyId);
        if(!isWorkingInCompany)
        {
            return;
        }

        context.Succeed(requirement);
    }
}
