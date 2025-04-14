using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Infrastructure")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace JobOffersApi.Modules.Users.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}