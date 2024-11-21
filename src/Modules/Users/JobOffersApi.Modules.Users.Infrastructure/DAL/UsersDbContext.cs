using Microsoft.EntityFrameworkCore;
using JobOffersApi.Infrastructure.Database;
using JobOffersApi.Modules.Users.Core.Entities;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL;

internal sealed class UsersDbContext : BaseDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    // public DbSet<InboxMessage> Inbox { get; set; }
    // public DbSet<OutboxMessage> Outbox { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}