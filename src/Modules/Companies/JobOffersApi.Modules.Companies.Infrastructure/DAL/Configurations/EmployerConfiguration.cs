using JobOffersApi.Modules.Companies.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOffersApi.Modules.JobOffers.Infrastructure.DAL.Configurations;

internal sealed class EmployerConfiguration : IEntityTypeConfiguration<Employer>
{
    public void Configure(EntityTypeBuilder<Employer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(maxLength: 150);

        builder.Property(x => x.LastName)
            .HasMaxLength(maxLength: 150)
            .IsRequired();

        builder.Property(x => x.DateOfBirth)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();
    }
}
