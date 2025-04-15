using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Companies.Core.Events;

internal record EmployerRemovedFromCompany(Guid UserId, Guid CompanyId) : IEvent;