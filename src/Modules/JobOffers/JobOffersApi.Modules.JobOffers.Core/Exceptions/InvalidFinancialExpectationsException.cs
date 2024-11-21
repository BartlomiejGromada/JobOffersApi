using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Exceptions;

internal class InvalidFinancialExpectationsException : ModularException
{
    public InvalidFinancialExpectationsException() : base($"Financial expectations can't be less or equal 0")
    {
    }
}
