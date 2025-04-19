using JobOffersApi.Abstractions.Events;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

namespace JobOffersApi.Modules.JobOffers.Core.Events;

internal record ChangedJobApplicationStatus(
    Guid UserId,
    Guid JobApplicationId,
    JobApplicationStatus Status) : IEvent;