using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Application.Commands.SignInCommand;

internal record SignInCommand(
    string Email,
    string Password) : ICommand
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
