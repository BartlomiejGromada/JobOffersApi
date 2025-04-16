using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Application.Commands.ChangePasswordCommand;

internal record ChangePasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword) : ICommand;