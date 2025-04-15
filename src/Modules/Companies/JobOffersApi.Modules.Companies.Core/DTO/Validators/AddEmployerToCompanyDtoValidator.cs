using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Companies.Core.DTO.Employers;

namespace JobOffersApi.Modules.Companies.Core.DTO.Validators;

internal sealed class AddEmployerToCompanyDtoValidator : AbstractValidator<AddEmployerToCompanyDto>
{
    public AddEmployerToCompanyDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage(Errors.Required);

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage(Errors.Required);
    }
}
