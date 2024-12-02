using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Infrastructure.Messaging.Outbox;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.Users.Infrastructure.DAL.Repositories;
using JobOffersApi.Modules.Users.Infrastructure.DAL;
using JobOffersApi.Modules.JobOffers.Core.Storages;
using JobOffersApi.Modules.JobOffers.Infrastructure.Storages;
using JobOffersApi.Modules.JobOffers.Core.Services;
using JobOffersApi.Modules.JobOffers.Infrastructure.Services;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace JobOffersApi.Modules.JobOffers.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
                .AddScoped<IJobOffersRepository, JobOffersRepository>()
                .AddScoped<IJobOffersStorage, JobOffersStorage>()
                .AddScoped<IJobApplicationsStorage, JobApplicationsStorage>()
                .AddScoped<IJobOffersService, JobOffersService>()
                .AddMsSqlServer<JobOffersDbContext>()
                .AddOutbox<JobOffersDbContext>()
                .AddUnitOfWork<JobOffersUnitOfWork>();
    }
}