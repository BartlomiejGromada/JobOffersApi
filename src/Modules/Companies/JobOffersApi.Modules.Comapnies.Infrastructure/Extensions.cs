using JobOffersApi.Infrastructure.Messaging.Outbox;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Core.Storages;
using JobOffersApi.Modules.Companies.Infrastructure.DAL;
using JobOffersApi.Modules.Companies.Infrastructure.DAL.Repositories;
using JobOffersApi.Modules.Users.Infrastructure.Storages;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace JobOffersApi.Modules.Companies.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
                .AddScoped<ICompaniesRepository, CompaniesRepository>()
                .AddScoped<ICompaniesStorage, CompaniesStorage>()
                .AddScoped<IEmployersRepository, EmployersRepository>()
                .AddMsSqlServer<CompaniesDbContext>()
                .AddOutbox<CompaniesDbContext>()
                .AddUnitOfWork<CompaniesUnitOfWork>();
    }
}