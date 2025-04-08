using Microsoft.EntityFrameworkCore;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Core.Entities;

namespace JobOffersApi.Modules.Companies.Infrastructure.DAL.Repositories;

internal sealed class EmployersRepository : IEmployersRepository
{
    private readonly CompaniesDbContext _context;
    private readonly DbSet<Employer> _employers;

    public EmployersRepository(CompaniesDbContext context)
    {
        _context = context;
        _employers = context.Employers;
    }

    public Task<Employer?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => _employers.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task AddAsync(Employer employer, CancellationToken cancellationToken = default)
    {
        await _employers.AddAsync(employer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}