using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.Validators;

internal class AddJobApplicationDtoValidator : AbstractValidator<AddJobApplicationDto>
{
    public AddJobApplicationDtoValidator(IClock clock)
    {
        RuleFor(x => x.MessageToEmployer)
         .MaximumLength(500)
         .WithMessage(Errors.MaxLengthExceeded(500));

        RuleFor(x => x.Availability)
            .NotNull().WithMessage(Errors.Required)
            .IsInEnum().WithMessage(Errors.InvalidValue);

        RuleFor(x => x.AvailabilityDate)
            .GreaterThanOrEqualTo(clock.CurrentDateOffset())
            .WithMessage(Errors.DateCantBePast);


        RuleFor(x => x.FinancialExpectations)
            .Must(x => x == null || x.Value > 0).WithMessage(Errors.GreaterThen(0));


        RuleFor(x => x.PreferredContract)
            .NotNull().WithMessage(Errors.Required)
            .IsInEnum().WithMessage(Errors.InvalidValue);

        RuleFor(x => x.CV)
            .NotNull()
            .WithMessage(Errors.Required);
    }
}