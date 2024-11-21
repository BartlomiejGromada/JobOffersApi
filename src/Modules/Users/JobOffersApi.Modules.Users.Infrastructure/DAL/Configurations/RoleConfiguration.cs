using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobOffersApi.Modules.Users.Core.Entities;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Name);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Permissions)
            .HasConversion(x => string.Join(',', x), x => x.Split(',', StringSplitOptions.None));

        builder
           .Property(x => x.Permissions)
           .HasField("_permissions")
           .HasConversion(
               x => string.Join(',', x),
               x => x.Split(',', StringSplitOptions.None).ToList()
           )
           .Metadata.SetValueComparer(
               new ValueComparer<IReadOnlyCollection<string>>(
                   (c1, c2) => c1.SequenceEqual(c2),
                   c => c.Aggregate(0, (a, next) => HashCode.Combine(a, next.GetHashCode())),
                   c => c.ToList())
           );
    }
}