using Microsoft.Extensions.Logging;
using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Application.Commands.SignOutCommand;

internal sealed class SignOutCommandHandler : ICommandHandler<SignOutCommand>
{
    private readonly ILogger<SignOutCommandHandler> _logger;

    public SignOutCommandHandler(ILogger<SignOutCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(SignOutCommand command, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        _logger.LogInformation($"User with ID: '{command.UserId}' has signed out.");
    }
}