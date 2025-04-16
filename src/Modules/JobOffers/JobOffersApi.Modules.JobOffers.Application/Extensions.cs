using System.Runtime.CompilerServices;
using JobOffersApi.Modules.JobOffers.Application.Services;
using JobOffersApi.Modules.JobOffers.Core.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Infrastructure")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace JobOffersApi.Modules.JobOffers.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationJobOfferService,  AuthorizationJobOfferService>();
        services.AddScoped<IAuthorizationJobApplicationService, AuthorizationJobApplicationService>();

        return services;
    }
}