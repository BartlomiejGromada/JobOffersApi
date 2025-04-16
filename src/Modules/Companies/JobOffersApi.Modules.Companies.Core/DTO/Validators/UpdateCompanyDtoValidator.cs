using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.DTO.Validators;
using JobOffersApi.Modules.Companies.Core.DTO.Companies;

namespace JobOffersApi.Modules.Companies.Core.DTO.Validators;

internal sealed class UpdateCompanyDtoValidator : AbstractValidator<UpdateCompanyDto>
{
    public UpdateCompanyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(Errors.Required);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(Errors.Required);


        RuleFor(x => x.Location)
            .SetValidator(new LocationValidator());
    }
}
