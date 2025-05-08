using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using JobOffersApi.Abstractions.Modules;
using JobOffersApi.Infrastructure;
using JobOffersApi.Infrastructure.Messaging.Outbox;
using JobOffersApi.Infrastructure.Modules;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Infrastructure.Services;

namespace JobOffersApi.Bootstrapper;

public class Startup
{
    private readonly IList<Assembly> _assemblies;
    private readonly IList<IModule> _modules;

    public Startup(IConfiguration configuration)
    {
        _assemblies = ModuleLoader.LoadAssemblies(configuration, "JobOffersApi.Modules.");
        _modules = ModuleLoader.LoadModules(_assemblies);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddModularInfrastructure(_assemblies, _modules);
        services.AddMsSqlServer();
        services.AddTransactionalDecorators();
        services.AddOutbox();
        services.AddHostedService<DbContextAppInitializer>();
        foreach (var module in _modules)
        {
            module.Register(services);
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        logger.LogInformation($"Modules: {string.Join(", ", _modules.Select(x => x.Name))}");
        app.UseModularInfrastructure(env);
        foreach (var module in _modules)
        {
            module.Use(app);
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync("Job Offers API"));
            endpoints.MapModuleInfo();
        });

        _assemblies.Clear();
        _modules.Clear();
    }
}