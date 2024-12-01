using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using Microsoft.AspNetCore.Http;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

internal class AddJobApplicationDto
{
    public string? MessageToEmployer { get; init; }
    public Availability Availability { get; init; }
    public DateTimeOffset? AvailabilityDate { get; init; }
    public AddFinancialExpectationsDto? FinancialExpectations { get; init; }
    public ContractType? PreferredContract { get; init; }
    public IFormFile CV { get; init; }
}