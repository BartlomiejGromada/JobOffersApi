using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersApi.Abstractions.Modules;

public interface IModule
{
    string Name { get; }
    IEnumerable<string> Policies => null;
    IEnumerable<IAuthorizationRequirement> AuthorizationRequirements => null;
    void Register(IServiceCollection services);
    void Use(IApplicationBuilder app);
}