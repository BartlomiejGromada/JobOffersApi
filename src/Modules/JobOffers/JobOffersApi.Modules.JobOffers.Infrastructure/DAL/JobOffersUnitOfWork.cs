using JobOffersApi.Infrastructure.MsSqlServer;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL;

internal class JobOffersUnitOfWork : MsSqlServerUnitOfWork<JobOffersDbContext>
{
    public JobOffersUnitOfWork(JobOffersDbContext dbContext) : base(dbContext)
    {
    }
}