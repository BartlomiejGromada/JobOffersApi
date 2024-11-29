using System;
using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Users.Core.Events;

internal record AppliedForJob(Guid UserId, Guid jobOfferId) : IEvent;