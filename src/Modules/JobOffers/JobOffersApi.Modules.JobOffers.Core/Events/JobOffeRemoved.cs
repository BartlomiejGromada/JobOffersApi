using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.JobOffers.Core.Events;

internal record JobOffeRemoved(Guid InvokerId, Guid jobOfferId) : IEvent;
