using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobOffersApi.Modules.Companies.Core.Entities;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL.Configurations;

internal sealed class CompanyEmployerConfiguration : IEntityTypeConfiguration<CompanyEmployer>
{
    public void Configure(EntityTypeBuilder<CompanyEmployer> builder)
    {
        builder.HasKey(x => new { x.CompanyId, x.EmployerId });

        builder.Property(x => x.Position)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany(y => y.CompaniesEmployers)
            .HasForeignKey(x => x.CompanyId);

        builder.HasOne(x => x.Employer)
            .WithMany(y => y.CompaniesEmployers)
            .HasForeignKey(x => x.EmployerId);


        builder.Navigation(c => c.Company)
            .AutoInclude();


        builder.Navigation(c => c.Employer)
            .AutoInclude();
    }
}