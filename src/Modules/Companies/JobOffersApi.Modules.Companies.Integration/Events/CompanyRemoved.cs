using JobOffersApi.Abstractions.Events;

namespace JobOffersApi.Modules.Companies.Integration.Events;

public record CompanyRemoved(Guid UserId, Guid CompanyId) : IEvent;