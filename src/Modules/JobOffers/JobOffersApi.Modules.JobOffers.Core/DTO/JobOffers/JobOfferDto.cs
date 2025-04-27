using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

internal class JobOfferDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LocationDto Location { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
    public List<FinancialConditionDto> FinancialCondition { get; set; }
    public List<JobAttributeDto> Attributes { get; set; }
}

internal class JobAttributeDto
{
    public JobAttributeType Type { get; init; }
    public string Name { get; init; }
}

internal class LocationDto
{
    public string Country { get; init; }
    public string City { get; init; }
    public string? Street { get; init; }
    public string HouseNumber { get; init; }
    public string? ApartmentNumber { get; init; }
    public string? PostalCode { get; init; }
}
