using JobOffersApi.Abstractions.Modules;
using JobOffersApi.Modules.Companies.Application;
using JobOffersApi.Modules.Companies.Core;
using JobOffersApi.Modules.Companies.Core.Policies.CompanyMembershipRequirement;
using JobOffersApi.Modules.Companies.Core.Policies.CompanyOwnershipRequirement;
using JobOffersApi.Modules.Companies.Infrastructure;
using JobOffersApi.Modules.Companies.Integration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JobsOfferApi.Modules.Companies.Api;

internal class CompaniesModule : IModule
{
    public string Name { get; } = "Companies";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "companies"
    };

    public IEnumerable<IAuthorizationRequirement> AuthorizationRequirements { get; } = new IAuthorizationRequirement[]
    {
        new CompanyOwnershipRequirement(), new CompanyMembershipRequirement()
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
        services.AddInfrastructure();
        services.AddApplication();
        services.AddIntegration();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}