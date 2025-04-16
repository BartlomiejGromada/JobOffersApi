using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Companies.Core.DTO.Employers;

namespace JobOffersApi.Modules.Companies.Core.DTO.Companies;

internal class CompanyDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Location Location { get; init; }
    public List<EmployerDto> Employers { get; init; }
}
