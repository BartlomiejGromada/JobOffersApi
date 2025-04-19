using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL.Configurations;

internal sealed class JobOfferConfiguration : IEntityTypeConfiguration<JobOffer>
{
    public void Configure(EntityTypeBuilder<JobOffer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(350)
            .IsRequired();

        builder.Property(x => x.DescriptionHtml)
            .IsRequired();

        builder.OwnsOne(x => x.Location, cb =>
        {
            cb.Property(y => y.Country) .HasMaxLength(100).IsRequired();
            cb.Property(y => y.City).HasMaxLength(100).IsRequired();
            cb.Property(y => y.Street).HasMaxLength(200).IsRequired(required: false);
            cb.Property(y => y.HouseNumber).HasMaxLength(20).IsRequired();
            cb.Property(y => y.ApartmentNumber).HasMaxLength(10).IsRequired(required: false);
            cb.Property(y => y.PostalCode).HasMaxLength(15).IsRequired(required: false);
        });

        builder.OwnsMany(x => x.FinancialConditions, cb =>
        {
            cb.OwnsOne(y => y.Value, vCb =>
            {
                vCb.Property(z => z.Currency).HasMaxLength(maxLength: 10).HasConversion<string>();
            });
            cb.Property(y => y.SalaryType).HasMaxLength(maxLength: 20).HasConversion<string>();
            cb.Property(y => y.SalaryPeriod).HasMaxLength(maxLength: 30).HasConversion<string>();
        });

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.ExpirationDate)
            .IsRequired();

        builder.Property(x => x.CompanyId)
            .IsRequired();

        builder.Property(x => x.CompanyName)
            .HasMaxLength(maxLength: 200)
            .IsRequired();

        builder.HasMany(x => x.JobApplications)
            .WithOne(y => y.JobOffer)
            .HasForeignKey(x => x.JobOfferId);

        builder.HasMany(x => x.JobAttributes)
            .WithMany(y => y.JobOffers);

        builder.Navigation(x => x.JobAttributes)
            .AutoInclude();
    }
}