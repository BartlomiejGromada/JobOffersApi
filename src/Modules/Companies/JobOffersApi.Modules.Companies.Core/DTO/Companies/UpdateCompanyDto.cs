using JobOffersApi.Abstractions.DTO;

namespace JobOffersApi.Modules.Companies.Core.DTO.Companies;

internal class UpdateCompanyDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public AddLocationDto Location { get; init; }
}
