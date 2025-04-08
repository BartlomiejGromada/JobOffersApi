using JobOffersApi.Infrastructure.MsSqlServer;

namespace JobOffersApi.Modules.Companies.Infrastructure.DAL;

internal sealed class CompaniesUnitOfWork : MsSqlServerUnitOfWork<CompaniesDbContext>
{
    public CompaniesUnitOfWork(CompaniesDbContext dbContext) : base(dbContext)
    {
    }
}