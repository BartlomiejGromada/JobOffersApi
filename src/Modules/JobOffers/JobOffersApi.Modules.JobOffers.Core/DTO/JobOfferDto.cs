using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;

namespace JobOffersApi.Modules.JobOffers.Core.DTO;

internal class JobOfferDto
{
    public string Title { get; set; }
    public Location Location { get; set; }
    public FinancialCondition? FinancialCondition { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
    public List<JobAttribute> Attributes { get; set; }
    public int ValidityInDays { get; set; }
}
