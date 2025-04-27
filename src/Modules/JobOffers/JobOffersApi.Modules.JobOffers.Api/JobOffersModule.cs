using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Abstractions.Modules;
using JobOffersApi.Modules.JobOffers.Core;
using JobOffersApi.Modules.JobOffers.Infrastructure;
using JobOffersApi.Modules.JobOffers.Application;

namespace JobsOfferApi.Modules.JobOffers.Api;

internal class JobOffersModule : IModule
{
    public string Name { get; } = "JobOffers";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "job-offers", "job-applications"
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
        services.AddInfrastructure();
        services.AddApplication();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}