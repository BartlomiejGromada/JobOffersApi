using JobOffersApi.Modules.Companies.Core.DTO.Employers;

namespace JobOffersApi.Modules.Companies.Core.Storages;

internal interface IEmployersStorage
{
    Task<EmployerDto?> GetByIdAsync(Guid employerId, CancellationToken cancellationToken = default);
}
