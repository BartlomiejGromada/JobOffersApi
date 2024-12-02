using JobOffersApi.Modules.Companies.Integration.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Api")]
namespace JobOffersApi.Modules.Companies.Integration;
internal static class Extensions
{
    public static IServiceCollection AddIntegration(this IServiceCollection services)
    {
        services.AddScoped<ICompaniesService, CompaniesService>();
        return services;
    }
}