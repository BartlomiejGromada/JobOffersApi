using FluentValidation;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Users.Core.DTO;

namespace JobOffersApi.Modules.Users.Core.Validators;

internal sealed class SignUpDtoValidator : AbstractValidator<SignUpDto>
{
    public SignUpDtoValidator(IClock clock)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(Errors.Required)
            .EmailAddress().WithMessage(Errors.InvalidValue);

        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator());

        RuleFor(x => x.RepeatPassword)
            .Equal(x => x.Password).WithMessage(Errors.MustBeEqual("password"));

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(Errors.Required)
            .MaximumLength(150).WithMessage(Errors.MaxLengthExceeded(150));

        RuleFor(x => x.LastName)
           .NotEmpty().WithMessage(Errors.Required)
           .MaximumLength(150).WithMessage(Errors.MaxLengthExceeded(150));

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage(Errors.InvalidValue);

        RuleFor(x => x.DateOfBirth)
            .NotNull().WithMessage(Errors.Required)
            .LessThan(clock.CurrentDateOffset()).WithMessage(Errors.DateCantBeFuture);
    }
}
