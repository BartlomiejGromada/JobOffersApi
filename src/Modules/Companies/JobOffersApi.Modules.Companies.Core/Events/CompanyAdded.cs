using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Companies.Core.Events;

internal record CompanyAdded(Guid UserId, Guid CompanyId) : IEvent;