using System;
using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Users.Core.Events
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;
}