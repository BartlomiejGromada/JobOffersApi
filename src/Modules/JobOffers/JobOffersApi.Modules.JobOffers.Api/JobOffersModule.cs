using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Abstractions.Modules;

namespace JobsOfferApi.Modules.First.Api;

internal class JobOffersModule : IModule
{
    public string Name { get; } = "JobOffers";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "jobOffers"
    };

    public void Register(IServiceCollection services)
    {
    }

    public void Use(IApplicationBuilder app)
    {
    }
}