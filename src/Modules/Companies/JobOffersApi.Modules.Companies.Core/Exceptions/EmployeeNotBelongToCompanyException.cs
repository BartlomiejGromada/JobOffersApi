using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Companies.Core.Exceptions;

internal class EmployeeNotBelongToCompanyException : ModularException
{
    public EmployeeNotBelongToCompanyException(Guid employerId, Guid companyId) 
        : base($"Employer with id: {employerId} not belong to company with id: {companyId}")
    {
    }
}
