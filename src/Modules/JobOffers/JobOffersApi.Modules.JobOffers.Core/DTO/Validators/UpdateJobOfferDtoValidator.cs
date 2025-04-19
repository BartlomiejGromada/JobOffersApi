using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.DTO.Validators;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.Validators;

internal class UpdateJobOfferDtoValidator : AbstractValidator<UpdateJobOfferDto>
{
    public UpdateJobOfferDtoValidator()
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