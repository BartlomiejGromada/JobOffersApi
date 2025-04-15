using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Companies.Core.Events;

internal record EmployerAddedToCompany(Guid UserId, Guid CompanyId) : IEvent;