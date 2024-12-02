using FluentValidation;
using JobOffersApi.Abstractions.Core;

namespace JobOffersApi.Modules.Users.Core.Validators;

internal sealed class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x)
           .NotEmpty().WithMessage(Errors.Required)
           .MinimumLength(8).WithMessage(Errors.MinLength(8))
           .Matches("[A-Z]").WithMessage(Errors.ContainsUpperCase)
           .Matches("[a-z]").WithMessage(Errors.ContainsLowerCase)
           .Matches("[0-9]").WithMessage(Errors.ContainsOneDigit)
           .Matches("[^a-zA-Z0-9]").WithMessage(Errors.ContainsOneSpecialCharacter);
    }
}
