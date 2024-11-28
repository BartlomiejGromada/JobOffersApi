using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Infrastructure")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace JobOffersApi.Modules.JobOffers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}