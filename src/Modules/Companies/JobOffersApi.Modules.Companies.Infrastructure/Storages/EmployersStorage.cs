using JobOffersApi.Modules.Companies.Core.DTO;
using JobOffersApi.Modules.Companies.Core.DTO.Extensions;
using JobOffersApi.Modules.Companies.Core.Entities;
using JobOffersApi.Modules.Companies.Core.Storages;
using JobOffersApi.Modules.Companies.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.Companies.Infrastructure.Storages;

internal sealed class EmployersStorage : IEmployersStorage
{
    private readonly IQueryable<Employer> _employers;

    public EmployersStorage(CompaniesDbContext dbContext)
    {
        _employers = dbContext.Employers.AsNoTracking();
    }

    public async Task<EmployerDto?> GetByIdAsync(Guid employerId, CancellationToken cancellationToken = default)
    {
        var employer = await _employers.FirstOrDefaultAsync(e => e.Id == employerId, cancellationToken);
        return employer?.ToDto();
    }
}
