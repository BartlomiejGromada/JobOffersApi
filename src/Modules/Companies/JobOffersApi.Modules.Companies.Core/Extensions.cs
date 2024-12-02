using System.Runtime.CompilerServices;
using FluentValidation;
using JobOffersApi.Modules.Companies.Core.DTO;
using JobOffersApi.Modules.Companies.Core.DTO.Validators;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Infrastructure")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace JobOffersApi.Modules.Companies.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddEmployerToCompanyDto>, AddEmployerToCompanyDtoValidator>();

        return services;
    }
}