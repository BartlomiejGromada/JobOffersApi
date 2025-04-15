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

        builder.Navigation(c => c.CompaniesEmployers)
            .AutoInclude();
    }
}