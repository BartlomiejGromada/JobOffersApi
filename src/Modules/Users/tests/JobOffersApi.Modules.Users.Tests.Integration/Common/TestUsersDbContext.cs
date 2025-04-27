using JobOffersApi.Modules.Users.Infrastructure.DAL;
using JobOffersApi.Shared.Tests;

namespace JobOffersApi.Modules.JobOffers.Tests.Integration.Common;

internal sealed class TestUsersDbContext(string connectionString) : TestDbContext<UsersDbContext>(connectionString)
{
    protected override UsersDbContext Init(string connectionString)
        => new(DbHelper.GetOptions<UsersDbContext>(connectionString));
}