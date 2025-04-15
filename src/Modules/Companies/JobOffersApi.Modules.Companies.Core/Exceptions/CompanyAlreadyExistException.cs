using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Modules.Companies.Core.Exceptions;

internal class CompanyAlreadyExistException : ModularException
{
    public CompanyAlreadyExistException(string name) : base($"Company with name: {name} already exist.")
    {
    }
}
