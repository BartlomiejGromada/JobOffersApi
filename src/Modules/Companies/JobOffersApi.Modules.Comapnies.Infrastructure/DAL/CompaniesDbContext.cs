using JobOffersApi.Infrastructure.Database;
using JobOffersApi.Modules.Companies.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.Companies.Infrastructure.DAL;

internal sealed class CompaniesDbContext : BaseDbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<CompanyEmployer> CompaniesEmployers { get; set; }

    // public DbSet<InboxMessage> Inbox { get; set; }
    // public DbSet<OutboxMessage> Outbox { get; set; }

    public CompaniesDbContext(DbContextOptions<CompaniesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("companies");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}