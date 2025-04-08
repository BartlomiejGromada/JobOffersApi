using System;
using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Users.Core.Events
{
    internal record UserStateUpdated(Guid UserId, string State) : IEvent;
}