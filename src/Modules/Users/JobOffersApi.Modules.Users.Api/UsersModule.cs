using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Modules.Users.Core;
using JobOffersApi.Abstractions.Modules;
using JobOffersApi.Modules.Users.Infrastructure;
using JobOffersApi.Modules.Users.Integration;
using JobOffersApi.Modules.Users.Application;

namespace JobOffersApi.Modules.Users.Api;

internal class UsersModule : IModule
{
    public string Name { get; } = "Users";
    
    public IEnumerable<string> Policies { get; } = new[]
    {
        "users"
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
        services.AddInfrastructure();
        services.AddApplication();
        services.AddIntegration();
    }
    
    public void Use(IApplicationBuilder app)
    {
    }
}