using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobOffersApi.Modules.Companies.Core.Entities;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL.Configurations;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.Description)
            .IsRequired();

        builder.Property(c => c.CreatedDate)
            .IsRequired();

        builder.OwnsOne(c => c.Location, cb =>
        {
            cb.Property(l => l.Country).HasMaxLength(100).IsRequired();
            cb.Property(l => l.City).HasMaxLength(100).IsRequired();
            cb.Property(l => l.Street).HasMaxLength(200).IsRequired(required: false);
            cb.Property(l => l.HouseNumber).HasMaxLength(20).IsRequired();
            cb.Property(l => l.ApartmentNumber).HasMaxLength(10).IsRequired(required: false);
            cb.Property(l => l.PostalCode).HasMaxLength(15).IsRequired(required: false);
        });

        builder.Navigation(c => c.CompaniesEmployers)
            .AutoInclude();
    }
}