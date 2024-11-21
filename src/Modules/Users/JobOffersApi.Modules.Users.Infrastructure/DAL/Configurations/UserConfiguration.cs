using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobOffersApi.Modules.Users.Core.Entities;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.State)
            .HasConversion<string>();

        builder.Property(x => x.DateOfBirth)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.HashedPassword)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(x => x.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(x => x.RoleId);

        builder.Navigation(x => x.Role)
            .AutoInclude();
    }
}