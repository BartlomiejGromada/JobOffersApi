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
}
