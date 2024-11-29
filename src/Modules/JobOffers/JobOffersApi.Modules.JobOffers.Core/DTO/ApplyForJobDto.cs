using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.Enums;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using Microsoft.AspNetCore.Http;

namespace JobOffersApi.Modules.JobOffers.Core.DTO;

internal class ApplyForJobDto
{
    public string? MessageToEmployer { get; init; }
    public Availability Availability { get; init; }
    public DateTimeOffset? AvailabilityDate { get; init; }
    public AddFinancialExpectationsDto? FinancialExpectations { get; init; }
    public ContractType? PreferredContract { get; init; }
    public IFormFile CV { get; init; }
}