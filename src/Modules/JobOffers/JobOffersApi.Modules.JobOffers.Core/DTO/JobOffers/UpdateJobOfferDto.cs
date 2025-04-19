using JobOffersApi.Abstractions.DTO;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

internal class UpdateJobOfferDto
{
    public string Title { get; init; }
    public string DescriptionHtml { get; init; }
    public AddLocationDto Location { get; init; }
    public Guid CompanyId { get; init; }
    public string CompanyName { get; init; }
    public List<AddJobAttribute> Attributes { get; init; }
    public List<AddFinancialExpectationsDto>? FinancialConditions { get; init; }
    public int? ValidityInDays { get; init; }
}