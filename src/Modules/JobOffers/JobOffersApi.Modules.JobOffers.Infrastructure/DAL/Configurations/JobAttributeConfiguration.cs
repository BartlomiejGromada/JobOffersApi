using JobOffersApi.Modules.JobOffers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOffersApi.Modules.JobOffers.Infrastructure.DAL.Configurations;

internal sealed class JobAttributeConfiguration : IEntityTypeConfiguration<JobAttribute>
{
    public void Configure(EntityTypeBuilder<JobAttribute> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(maxLength: 150)
            .HasConversion<string>();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(maxLength: 250);
    }
}
