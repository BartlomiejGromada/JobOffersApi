using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Abstractions.Modules;

namespace JobsOfferApi.Modules.First.Api
{
    internal class FirstModule : IModule
    {
        public string Name { get; } = "First";

        public IEnumerable<string> Policies { get; } = new[]
        {
            "first"
        };

        public void Register(IServiceCollection services)
        {
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}