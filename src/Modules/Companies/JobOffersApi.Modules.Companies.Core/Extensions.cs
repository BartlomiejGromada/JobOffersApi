using System.Runtime.CompilerServices;
using FluentValidation;
using JobOffersApi.Modules.Companies.Core.DTO.Validators;
using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Modules.Companies.Core.DTO.Employers;
using JobOffersApi.Modules.Companies.Core.DTO.Companies;
using Microsoft.AspNetCore.Authorization;
using JobOffersApi.Modules.Companies.Core.Policies.CompanyOwnershipRequirement;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Infrastructure")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Application")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.Companies.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace JobOffersApi.Modules.Companies.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddEmployerToCompanyDto>, AddEmployerToCompanyDtoValidator>();
        services.AddTransient<IValidator<AddCompanyDto>, AddCompanyDtoValidator>();

        services.AddScoped<IAuthorizationHandler, CompanyMembershipRequirementHandler>();

        return services;
    }
}