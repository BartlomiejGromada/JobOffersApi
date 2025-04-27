using JobOffersApi.Modules.Users.Infrastructure.DAL;
using JobOffersApi.Shared.Tests;

namespace JobOffersApi.Modules.JobOffers.Tests.Integration.Common;

internal sealed class TestJobOffersDbContext(string connectionString) : TestDbContext<JobOffersDbContext>(connectionString)
{
    protected override JobOffersDbContext Init(string connectionString)
        => new(DbHelper.GetOptions<JobOffersDbContext>(connectionString));
}