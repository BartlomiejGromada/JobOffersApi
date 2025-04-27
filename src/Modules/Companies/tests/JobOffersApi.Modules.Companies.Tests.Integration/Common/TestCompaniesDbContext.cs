using JobOffersApi.Modules.Companies.Infrastructure.DAL;
using JobOffersApi.Shared.Tests;

namespace JobOffersApi.Modules.Companies.Tests.Integration.Common;

internal sealed class TestCompaniesDbContext(string connectionString) : TestDbContext<CompaniesDbContext>(connectionString)
{
    protected override CompaniesDbContext Init(string connectionString)
        => new(DbHelper.GetOptions<CompaniesDbContext>(connectionString));
}