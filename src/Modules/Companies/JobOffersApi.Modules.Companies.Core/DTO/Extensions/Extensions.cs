using JobOffersApi.Modules.Companies.Core.DTO.Companies;
using JobOffersApi.Modules.Companies.Core.DTO.Employers;
using JobOffersApi.Modules.Companies.Core.Entities;

namespace JobOffersApi.Modules.Companies.Core.DTO.Extensions;

internal static class Extensions
{
    public static EmployerDto ToDto(this Employer employer)
        => new()
        {
            Id = employer.Id,
            FirstName = employer.FirstName,
            LastName = employer.LastName,
            DateOfBirth = employer.DateOfBirth,
            CreatedDate = employer.CreatedDate,
        };

    public static CompanyDto ToDto(this Company company)
        => new()
        {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description,
            Employers = company.CompaniesEmployers.Select(ce => ce.Employer.ToDto()).ToList(),
        };
}
