using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Abstractions.Kernel;

namespace JobOffersApi.Infrastructure.Kernel;

public static class Extensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>(), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}