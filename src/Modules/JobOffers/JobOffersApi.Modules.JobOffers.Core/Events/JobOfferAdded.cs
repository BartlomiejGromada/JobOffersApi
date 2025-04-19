using System;
using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Users.Core.Events;

internal record JobOfferAdded(Guid UserId, Guid JobOfferId) : IEvent;