using JobOffersApi.Modules.JobOffers.Core.DTO;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;

namespace JobOffersApi.Modules.JobOffers.Core.Entities.Extensions;

internal static class Extensions
{
    public static JobOfferDto AsDto(this JobOffer jobOffer)
        => jobOffer.Map<JobOfferDto>();

    public static JobOfferDetailsDto AsDetailsDto(this JobOffer jobOffer)
    {
        var dto = jobOffer.Map<JobOfferDetailsDto>();
        dto.DescriptionHtml = jobOffer.DescriptionHtml;
        return dto;
    }

    private static T Map<T>(this JobOffer jobOffer) where T : JobOfferDto, new()
        => new()
        {
            Title = jobOffer.Title,
            Location = jobOffer.Location,
            FinancialCondition = jobOffer.FinancialCondition,
            CreatedAt = jobOffer.CreatedAt,
            CompanyId = jobOffer.CompanyId,
            CompanyName = jobOffer.CompanyName,
            Attributes = jobOffer.JobAttributes.ToList(),
            ValidityInDays = jobOffer.ValidityInDays
        };

    public static FinancialCondition ToValueObject(this AddFinancialExpectationsDto dto)
        => new(
            dto.Value,
            dto.ConcurrencyCode,
            dto.SalaryType,
            dto.SalaryPeriod);
}
