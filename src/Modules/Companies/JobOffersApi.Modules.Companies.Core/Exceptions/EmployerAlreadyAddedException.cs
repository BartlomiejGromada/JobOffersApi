using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Companies.Core.Exceptions;

internal class EmployerAlreadyAddedException : ModularException
{
    public EmployerAlreadyAddedException(Guid companyId, Guid employerId) : base(
        $"Employer with id: {employerId} already working in company with id: {companyId}.")
    {
    }
}
