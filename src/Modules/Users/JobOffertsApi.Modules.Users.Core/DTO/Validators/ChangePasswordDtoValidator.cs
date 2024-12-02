using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Users.Core.DTO;

namespace JobOffersApi.Modules.Users.Core.Validators;

internal sealed class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(Errors.Required)
            .EmailAddress().WithMessage(Errors.InvalidValue);

        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage(Errors.Required);

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(Errors.Required);

        RuleFor(x => x.NewPassword)
           .SetValidator(new PasswordValidator());
    }
}
