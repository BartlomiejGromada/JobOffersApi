using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Companies.Core.Events;

internal record CompanyRemoved(Guid UserId, Guid CompanyId) : IEvent;