using Microsoft.EntityFrameworkCore;
using JobOffersApi.Infrastructure.Converters;
using System;

namespace JobOffersApi.Infrastructure.Database;

public abstract class BaseDbContext: DbContext
{
    protected BaseDbContext(DbContextOptions options) : base(options) { }


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateOnly>()
                            .HaveConversion<DateOnlyConverter>()
                            .HaveColumnType("date");
    }
}
