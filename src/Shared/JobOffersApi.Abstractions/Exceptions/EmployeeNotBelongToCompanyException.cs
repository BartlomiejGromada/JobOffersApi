using System;

namespace JobOffersApi.Abstractions.Exceptions;

public class EmployeeNotBelongToCompanyException : ModularException
{
    public EmployeeNotBelongToCompanyException(Guid employerId, Guid companyId)
        : base($"Employer with id: {employerId} not belong to company with id: {companyId}")
    {
    }
}
