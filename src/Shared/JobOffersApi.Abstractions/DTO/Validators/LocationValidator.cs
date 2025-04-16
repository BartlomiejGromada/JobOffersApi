using FluentValidation;
using JobOffersApi.Abstractions.Core;

namespace JobOffersApi.Abstractions.DTO.Validators;

public class LocationValidator : AbstractValidator<AddLocationDto>
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