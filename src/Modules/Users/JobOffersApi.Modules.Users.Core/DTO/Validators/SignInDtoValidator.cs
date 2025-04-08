using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Users.Core.DTO;

namespace JobOffersApi.Modules.Users.Core.Validators;

internal sealed class SignInDtoValidator : AbstractValidator<SignInDto>
{
    public SignInDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(Errors.Required)
            .EmailAddress().WithMessage(Errors.InvalidValue);

        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator());
    }
}
