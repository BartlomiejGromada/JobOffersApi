using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Companies.Core.Events;

internal record CompanyUpdated(Guid UserId, Guid CompanyId) : IEvent;
