using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Companies.Core.Exceptions;

internal class CompanyNotFoundException : ModularException
{
    public CompanyNotFoundException(Guid companyId) : base($"Company with id: {companyId} not found.")
    {
    }
}
