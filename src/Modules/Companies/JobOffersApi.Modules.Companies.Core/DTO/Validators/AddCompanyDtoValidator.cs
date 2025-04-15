using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Companies.Core.DTO.Companies;

namespace JobOffersApi.Modules.Companies.Core.DTO.Validators;

internal sealed class AddCompanyDtoValidator : AbstractValidator<AddCompanyDto>
{
    public AddCompanyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(Errors.Required)
            .EmailAddress().WithMessage(Errors.InvalidValue);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(Errors.Required);
    }
}
