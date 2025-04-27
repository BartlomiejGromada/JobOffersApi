using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.DTO;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.Extensions;

internal static class Extensions
{
    public static JobOfferDto ToDto(this JobOffer jobOffer)
        => jobOffer.Map<JobOfferDto>();

    public static JobOfferDetailsDto ToDetailsDto(this JobOffer jobOffer)
    {
        var dto = jobOffer.Map<JobOfferDetailsDto>();
        dto.DescriptionHtml = jobOffer.DescriptionHtml;
        return dto;
    }

    private static T Map<T>(this JobOffer jobOffer) where T : JobOfferDto, new()
        => new()
        {
            Id = jobOffer.Id,
            Title = jobOffer.Title,
            Location = jobOffer.Location.ToDto(),
            CreatedDate = jobOffer.CreatedDate,
            ExpirationDate = jobOffer.ExpirationDate,
            CompanyId = jobOffer.CompanyId,
            CompanyName = jobOffer.CompanyName,
            Attributes = jobOffer.JobAttributes.Select(ja => ja.ToDto()).ToList(),
            FinancialCondition = jobOffer.FinancialConditions.Select(fc => fc.ToDto()).ToList()
        };

    public static LocationDto ToDto(this Location location)
        => new() 
        {
            Country = location.Country,
            City = location.City,
            HouseNumber = location.HouseNumber,
            Street = location.Street,
            ApartmentNumber = location.ApartmentNumber,
            PostalCode = location.PostalCode
        };

    public static JobAttributeDto ToDto(this JobAttribute ja)
        => new()
        {
            Type = ja.Type,
            Name = ja.Name
        };

    public static DispositionDto ToDto(this Disposition dis)
        => new()
        {
            Availability = dis.Availability,
            Date = dis.Date,
        };

    public static FinancialConditionDto ToDto(this FinancialCondition fc)
        => new()
        {
            SalaryPeriod = fc.SalaryPeriod,
            SalaryType = fc.SalaryType,
            Value = fc.Value
        };

    public static JobOffer ToEntity(this AddJobOfferDto dto, DateTimeOffset createdAt)
    {
        var financialConditions = dto.FinancialConditions?.Select(f => f.ToValueObject())
           .ToList();

        var attributes = dto.Attributes
            .Select(a => a.ToEntity())
            .ToList();

        var jobOffer = new JobOffer(
            dto.Title,
            dto.DescriptionHtml,
            dto.Location.ToValueObject(),
            createdAt,
            dto.CompanyId,
            dto.CompanyName,
            attributes,
            financialConditions,
            dto.ValidityInDays);

        return jobOffer;
    }

    public static FinancialCondition ToValueObject(this AddFinancialExpectationsDto dto)
        => new(
            dto.Value,
            dto.ConcurrencyCode,
            dto.SalaryType,
            dto.SalaryPeriod);

    public static Location ToValueObject(this AddLocationDto dto)
            => new(
                dto.Country,
                dto.City,
                dto.HouseNumber,
                dto.Street,
                dto.ApartmentNumber,
                dto.PostalCode);

    public static JobAttribute ToEntity(this AddJobAttribute dto)
        => new(dto.Type, dto.Name);

    public static JobApplicationDto ToDto(this JobApplication ja)
        => new()
        {
            CandidateId = ja.CandidateId,
            CandidateFirstName = ja.CandidateFirstName,
            CandidateLastName = ja.CandidateLastName,
            MessageToEmployer = ja.MessageToEmployer,
            Disposition = new()
            {
                Availability = ja.Disposition.Availability,
                Date = ja.Disposition.Date,
            },
            FinancialExpectations = ja.FinancialExpectations?.ToDto(),
            PreferredContract = ja.PreferredContract,
            Status = ja.Status,
            CreatedAt = ja.CreatedAt,
        };
}
