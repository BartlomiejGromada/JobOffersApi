using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersApi.Infrastructure.Versioning;

public static class Extensions
{
    public static IServiceCollection AddVersioningForApi(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        })
         .AddMvc()
         .AddApiExplorer(options =>
         {
             options.GroupNameFormat = "'v'V";
             options.SubstituteApiVersionInUrl = true;
         });

        return services;
    }
}