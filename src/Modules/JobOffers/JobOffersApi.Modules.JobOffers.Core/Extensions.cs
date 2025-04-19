using System.Runtime.CompilerServices;
using FluentValidation;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.DTO.Validators;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Api")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Infrastructure")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Application")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Tests.Integration")]
[assembly: InternalsVisibleTo("JobOffersApi.Modules.JobOffers.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace JobOffersApi.Modules.JobOffers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddJobOfferDto>, AddJobOfferDtoValidator>();
        services.AddTransient<IValidator<UpdateJobOfferDto>, UpdateJobOfferDtoValidator>();
        services.AddTransient<IValidator<AddJobApplicationDto>, AddJobApplicationDtoValidator>();

        return services;
    }
}