using System;
using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Users.Core.Events;

internal record UnappliedFromJob(Guid UserId, Guid jobOfferId) : IEvent;