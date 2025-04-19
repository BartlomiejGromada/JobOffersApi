using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.JobOffers.Core.Events;

internal record JobOfferUpdated(Guid UserId, Guid JobOfferId) : IEvent;
