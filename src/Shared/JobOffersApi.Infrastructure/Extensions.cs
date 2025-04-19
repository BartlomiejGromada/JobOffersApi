using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Modules;
using JobOffersApi.Abstractions.Storage;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Infrastructure.Api;
using JobOffersApi.Infrastructure.Auth;
using JobOffersApi.Infrastructure.Commands;
using JobOffersApi.Infrastructure.Contexts;
using JobOffersApi.Infrastructure.Dispatchers;
using JobOffersApi.Infrastructure.Events;
using JobOffersApi.Infrastructure.Exceptions;
using JobOffersApi.Infrastructure.Kernel;
using JobOffersApi.Infrastructure.Logging;
using JobOffersApi.Infrastructure.Messaging;
using JobOffersApi.Infrastructure.Messaging.Outbox;
using JobOffersApi.Infrastructure.Modules;
using JobOffersApi.Infrastructure.Queries;
using JobOffersApi.Infrastructure.Security;
using JobOffersApi.Infrastructure.Serialization;
using JobOffersApi.Infrastructure.Storage;
using JobOffersApi.Infrastructure.Time;
using JobOffersApi.Infrastructure.Converters;
using System.Text.Json.Serialization;
using JobOffersApi.Abstractions.Helpers;
using JobOffersApi.Infrastructure.Helpers;
using FluentValidation.AspNetCore;
using JobOffersApi.Infrastructure.Versioning;
using JobOffersApi.Infrastructure.Swagger;
using Asp.Versioning.ApiExplorer;

namespace JobOffersApi.Infrastructure;

public static class Extensions
{
    private const string CorrelationIdKey = "correlation-id";
        
    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
        => services.AddTransient<IInitializer, T>();
        
    public static IServiceCollection AddModularInfrastructure(this IServiceCollection services,
        IList<Assembly> assemblies, IList<IModule> modules) 
    {
        var disabledModules = new List<string>();
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                {
                    continue;
                }

                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }
        }

        services.AddCorsPolicy();

        services.AddEndpointsApiExplorer();
        services.AddVersioningForApi();
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        var appOptions = services.GetOptions<AppOptions>("app");
        services.AddSingleton(appOptions);

        services.AddMemoryCache();
        services.AddHttpClient();
        services.AddSingleton<IRequestStorage, RequestStorage>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddSingleton<IFileHelper, FileHelper>();
        services.AddModuleInfo(modules);
        services.AddModuleRequests(assemblies);
        services.AddAuth(modules);
        services.AddErrorHandling();
        services.AddContext();
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        services.AddEvents(assemblies);
        services.AddDomainEvents(assemblies);
        services.AddMessaging();
        services.AddSecurity();
        services.AddOutbox();
        services.AddSingleton<IClock, UtcClock>();
        services.AddSingleton<IDispatcher, InMemoryDispatcher>();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }
                    
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            })
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                 options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
             });

        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });

        return services;
    }

    public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });
        app.UseCors("cors");
        app.UseCorrelationId();
        app.UseErrorHandling();

        if (env.IsDevelopment())
        {
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        app.UseAuth();
        app.UseContext();
        app.UseLogging();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    public static string GetModuleName(this object value)
        => value?.GetType().GetModuleName() ?? string.Empty;

    public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace.Contains(namespacePart)
            ? type.Namespace.Split(".")[splitIndex].ToLowerInvariant()
            : string.Empty;
    }
        
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        => app.Use((ctx, next) =>
        {
            ctx.Items.Add(CorrelationIdKey, Guid.NewGuid());
            return next();
        });
        
    public static Guid? TryGetCorrelationId(this HttpContext context)
        => context.Items.TryGetValue(CorrelationIdKey, out var id) ? (Guid) id : null;
}