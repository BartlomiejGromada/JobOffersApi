using System;

namespace JobOffersApi.Abstractions.Exceptions;

public class UnauthorizedCompanyAccessException : ModularException
{
    public UnauthorizedCompanyAccessException(Guid companyId, Guid userId) : base($"User with id: {userId} doesn't have access" +
        $"to company with id: {companyId}")
    {
    }
}
