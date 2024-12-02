using Microsoft.EntityFrameworkCore;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Core.Entities;

namespace JobOffersApi.Modules.Companies.Infrastructure.DAL.Repositories;

internal sealed class CompaniesRepository : ICompaniesRepository
{
    private readonly CompaniesDbContext _context;
    private readonly DbSet<Company> _companies;

    public CompaniesRepository(CompaniesDbContext context)
    {
        _context = context;
        _companies = context.Companies;
    }

    public Task<Company?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => _companies
            .Include(c => c.CompaniesEmployers)
            .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task UpdateAsync(Company company, CancellationToken cancellationToken = default)
    {
        _companies.Update(company);
        await _context.SaveChangesAsync(cancellationToken);
    }
}