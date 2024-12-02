using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Core.Commands;

internal record ChangePasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword) : ICommand;