﻿using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Infrastructure.Events;

public static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>(), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}