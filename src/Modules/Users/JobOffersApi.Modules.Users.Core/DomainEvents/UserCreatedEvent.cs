using JobOffersApi.Abstractions.Kernel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersApi.Modules.Users.Core.DomainEvents;

/// <summary>
/// Example of domain event
/// </summary>
/// <param name="UserId"></param>
internal record UserCreatedEvent(Guid UserId) : IDomainEvent;

internal sealed class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    public Task HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken = default)
    {
       // Example of handling raised domain event
        return Task.CompletedTask;
    }
}
