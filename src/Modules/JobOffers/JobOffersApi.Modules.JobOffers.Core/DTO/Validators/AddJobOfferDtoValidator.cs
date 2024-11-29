using FluentValidation;
using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.Validators;

internal class AddJobOfferDtoValidator : AbstractValidator<AddJobOfferDto>
{
    public AddJobOfferDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("ewew");

        RuleFor(x => x.DescriptionHtml)
            .NotEmpty()
            .WithMessage("ewewew");

        RuleFor(x => x.Location)
            .SetValidator(new LocationValidator());

        RuleFor(x => x.FinancialCondition)
            .Must(x =>
            {
                if (x is null)
                {
                    return true;
                }

                return x.Value != null
                   && x.SalaryType != null
                   && x.SalaryPeriod != null;
            })
            .WithMessage("qqqq")
            .ChildRules(value =>
             {
                 value.RuleFor(x => x.Value)
                      .GreaterThan(0)
                      .WithMessage("Value must be greater than 0.");
             });

        RuleFor(x => x.CompanyId)
            .NotNull()
            .WithMessage("company-id-must-be-not-null");

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("company-name-must-be-not-empty");

        RuleFor(x => x.Attributes)
            .NotEmpty()
            .WithMessage("attributes-list-must-contain-at-least-one-element");

        RuleFor(x => x.ValidityInDays)
            .Must(x =>
            {
                if (x is null)
                {
                    return true;
                }

                return x > 0;
            });
    }
}

internal class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {
        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required.")
            .MaximumLength(100)
            .WithMessage("Country name cannot exceed 100 characters.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required.")
            .MaximumLength(100)
            .WithMessage("City name cannot exceed 100 characters.");

        RuleFor(x => x.Street)
            .MaximumLength(100)
            .WithMessage("Street name cannot exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Street));

        RuleFor(x => x.HouseNumber)
            .NotEmpty()
            .WithMessage("House number is required.")
            .MaximumLength(20)
            .WithMessage("House number cannot exceed 20 characters.");

        RuleFor(x => x.ApartmentNumber)
            .MaximumLength(10)
            .WithMessage("Apartment number cannot exceed 10 characters.")
            .When(x => !string.IsNullOrEmpty(x.ApartmentNumber));

        RuleFor(x => x.PostalCode)
            .MaximumLength(20)
            .WithMessage("Postal code cannot exceed 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.PostalCode));
    }
}