using System;
using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Users.Core.Events;

internal record SignedIn(Guid UserId) : IEvent;