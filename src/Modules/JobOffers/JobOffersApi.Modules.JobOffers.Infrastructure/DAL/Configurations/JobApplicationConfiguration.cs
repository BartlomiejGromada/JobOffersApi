using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL.Configurations;

internal sealed class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CandidateId)
            .IsRequired();

        builder.Property(x => x.CandidateFirstName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.CandidateLastName)
           .IsRequired()
           .HasMaxLength(150);

        builder.HasOne(x => x.JobOffer)
            .WithMany(y => y.JobApplications)
            .HasForeignKey(x => x.JobOfferId);

        builder.Property(x => x.MessageToEmployer)
            .IsRequired(required: false)
            .HasMaxLength(maxLength: 500);

        builder.OwnsOne(x => x.Disposition, cb =>
        {
            cb.Property(y => y.Availability).HasMaxLength(maxLength: 50).HasConversion<string>();
            cb.Property(y => y.Date).IsRequired(required: false);
        });

        builder.OwnsOne(x => x.FinancialExpectations, cb =>
        {
            cb.OwnsOne(y => y.Value, vCb =>
            {
                vCb.Property(z => z.Currency).HasMaxLength(maxLength: 10).HasConversion<string>();
            });
            cb.Property(y => y.SalaryType).HasMaxLength(maxLength: 20).HasConversion<string>();
            cb.Property(y => y.SalaryPeriod).HasMaxLength(maxLength: 30).HasConversion<string>();
        });

        builder.Property(x => x.PreferredContract)
            .IsRequired(required: false)
            .HasMaxLength(maxLength: 80)
            .HasConversion<string>();

        builder.Property(x => x.Status)
            .HasMaxLength(maxLength: 50)
            .HasConversion<string>();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CV)
            .IsRequired();
    }
}