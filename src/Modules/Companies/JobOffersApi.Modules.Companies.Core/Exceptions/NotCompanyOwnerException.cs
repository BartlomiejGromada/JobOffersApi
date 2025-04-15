using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Companies.Core.Exceptions;

internal class NotCompanyOwnerException : ForbiddenException
{
    public NotCompanyOwnerException(Guid userId, Guid companyId) : base($"User with id: {userId} is not owner of the" +
        $"company with id: {companyId}")
    {
    }

    public NotCompanyOwnerException(Guid userId) : base($"User with id: {userId} is not owner of company")
    {
    }
}
