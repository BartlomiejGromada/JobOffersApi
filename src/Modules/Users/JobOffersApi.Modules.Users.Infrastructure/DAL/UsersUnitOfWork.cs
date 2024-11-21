using JobOffersApi.Infrastructure.MsSqlServer;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL;

internal class UsersUnitOfWork : MsSqlServerUnitOfWork<UsersDbContext>
{
    public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
    {
    }
}