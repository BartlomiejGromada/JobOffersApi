using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.Validators;

internal class AddJobOfferDtoValidator : AbstractValidator<AddJobOfferDto>
{
    public AddJobOfferDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(errorMessage: Errors.Required)
            .MaximumLength(300).WithMessage(Errors.MaxLengthExceeded(300));

        RuleFor(x => x.DescriptionHtml)
            .NotEmpty().WithMessage(errorMessage: Errors.Required);

        RuleFor(x => x.Location)
            .SetValidator(new LocationValidator());

        RuleFor(x => x.FinancialConditions)
            .Must(x => x.All(v => v.Value > 0)).WithMessage(errorMessage: Errors.GreaterThen(0))
            .When(x => x is not null); ;

        RuleFor(x => x.CompanyId)
            .NotNull().WithMessage(errorMessage: Errors.Required);

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage(errorMessage: Errors.Required);

        RuleFor(x => x.Attributes)
            .NotEmpty().WithMessage(errorMessage: Errors.Required);

        RuleFor(x => x.ValidityInDays)
            .Must(x =>
            {
                if (x is null)
                {
                    return true;
                }

                return x > 0;
            }).WithMessage(errorMessage: Errors.GreaterThen(0));
    }
}

internal class LocationValidator : AbstractValidator<AddLocationDto>
{
    public LocationValidator()
    {
        RuleFor(x => x.Country)
            .NotEmpty().WithMessage(errorMessage: Errors.Required)
            .MaximumLength(100).WithMessage(Errors.MaxLengthExceeded(100));

        RuleFor(x => x.City)
            .NotEmpty().WithMessage(errorMessage: Errors.Required)
            .MaximumLength(100).WithMessage(Errors.MaxLengthExceeded(100));

        RuleFor(x => x.Street)
            .MaximumLength(200).WithMessage(errorMessage: Errors.MaxLengthExceeded(200))
            .When(x => !string.IsNullOrEmpty(x.Street));

        RuleFor(x => x.HouseNumber)
            .NotEmpty().WithMessage(errorMessage: Errors.Required)
            .MaximumLength(20).WithMessage(Errors.MaxLengthExceeded(20));

        RuleFor(x => x.ApartmentNumber)
            .MaximumLength(10).WithMessage(errorMessage: Errors.MaxLengthExceeded(10))
            .When(x => !string.IsNullOrEmpty(x.ApartmentNumber));

        RuleFor(x => x.PostalCode)
            .MaximumLength(20).WithMessage(errorMessage: Errors.MaxLengthExceeded(20))
            .When(x => !string.IsNullOrEmpty(x.PostalCode));
    }
}