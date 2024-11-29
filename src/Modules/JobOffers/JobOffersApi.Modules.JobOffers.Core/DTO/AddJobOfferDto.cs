using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.JobOffers.Core.DTO;

internal class AddJobOfferDto
{
    public string Title { get; init; }
    public string DescriptionHtml { get; init; }
    public Location Location { get; init; }
    public AddFinancialExpectationsDto? FinancialCondition { get; init; }
    public Guid CompanyId { get; init; }
    public string CompanyName { get; init; }
    public List<JobAttribute> Attributes { get; init; }
    public int? ValidityInDays { get; init; }
}