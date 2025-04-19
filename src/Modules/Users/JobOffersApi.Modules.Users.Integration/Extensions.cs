using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Api")]
namespace JobOffersApi.Modules.Users.Integration;
internal static class Extensions
{
    public static IServiceCollection AddIntegration(this IServiceCollection services)
    {
        return services;
    }
}