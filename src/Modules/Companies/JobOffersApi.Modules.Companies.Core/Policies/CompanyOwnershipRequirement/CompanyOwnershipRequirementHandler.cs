using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Modules.Companies.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace JobOffersApi.Modules.Companies.Core.Policies.CompanyOwnershipRequirement;

internal sealed class CompanyOwnershipRequirementHandler : AuthorizationHandler<CompanyOwnershipRequirement>
{
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyOwnershipRequirementHandler(
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
        CompanyOwnershipRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var identity = _context.Identity;

        if (!httpContext.Request.RouteValues.TryGetValue("id", out var companyIdValue) ||
          !Guid.TryParse(companyIdValue?.ToString(), out Guid companyId))
        {
            return;
        }

        var isCompanyOwner = await _authorizationCompanyService.IsCompanyOwnerAsync(identity.Id, companyId);
        if(!isCompanyOwner)
        {
            return;
        }

        context.Succeed(requirement);
    }
}
