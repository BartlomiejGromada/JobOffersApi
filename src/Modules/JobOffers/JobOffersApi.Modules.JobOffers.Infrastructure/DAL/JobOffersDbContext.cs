using Microsoft.EntityFrameworkCore;
using JobOffersApi.Infrastructure.Database;
using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL;

internal sealed class JobOffersDbContext : BaseDbContext
{
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    public DbSet<JobAttribute> JobAttributes { get; set; }

    // public DbSet<InboxMessage> Inbox { get; set; }
    // public DbSet<OutboxMessage> Outbox { get; set; }

    public JobOffersDbContext(DbContextOptions<JobOffersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("jobOffers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}