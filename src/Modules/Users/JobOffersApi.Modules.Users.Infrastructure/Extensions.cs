using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Modules.Users.Core.Repositories;
using JobOffersApi.Infrastructure;
using JobOffersApi.Infrastructure.Messaging.Outbox;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Modules.Users.Infrastructure.DAL.Repositories;
using JobOffersApi.Modules.Users.Infrastructure.DAL;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Modules.Users.Infrastructure.Storages;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Tests.Unit")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Users.Tests.Integration")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace JobOffersApi.Modules.Users.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
                .AddSingleton<IUserRequestStorage, UserRequestStorage>()
                .AddScoped<IRolesRepository, RolesRepository>()
                .AddScoped<IUsersRepository, UsersRepository>()
                .AddScoped<IUsersStorage, UsersStorage>()
                .AddMsSqlServer<UsersDbContext>()
                .AddOutbox<UsersDbContext>()
                .AddUnitOfWork<UsersUnitOfWork>()
                .AddInitializer<UsersInitializer>();
    }
}