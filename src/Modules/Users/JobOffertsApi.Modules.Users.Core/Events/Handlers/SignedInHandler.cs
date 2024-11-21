using JobOffersApi.Abstractions.Events;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersApi.Modules.Users.Core.Events;

/// <summary>
/// Example of event handler
/// </summary>
internal class SignedInHandler : IEventHandler<SignedIn>
{
    private readonly ILogger<SignedIn> _logger;

    public SignedInHandler(ILogger<SignedIn> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(SignedIn @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Test");
        return Task.CompletedTask;
    }
}
