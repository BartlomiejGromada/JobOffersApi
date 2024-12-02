using JobOffersApi.Abstractions.Modules;
using JobOffersApi.Modules.Companies.Core;
using JobOffersApi.Modules.Companies.Infrastructure;
using JobOffersApi.Modules.Companies.Integration;
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

    public void Register(IServiceCollection services)
    {
        services.AddCore();
        services.AddInfrastructure();
        services.AddIntegration();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}