using JobOffersApi.Modules.Users.Integration.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Api")]
namespace JobOffersApi.Modules.Users.Integration;
internal static class Extensions
{
    public static IServiceCollection AddIntegration(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        return services;
    }
}