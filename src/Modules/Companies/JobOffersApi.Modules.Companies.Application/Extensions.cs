using System.Runtime.CompilerServices;
using JobOffersApi.Modules.Companies.Application.Services;
using JobOffersApi.Modules.Companies.Core.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Infrastructure")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace JobOffersApi.Modules.Companies.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationCompanyService, AuthorizationCompanyService>();

        return services;
    }
}