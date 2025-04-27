using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobOffersApi.Shared.Tests;

public static class DbHelper
{
    private static readonly IConfiguration Configuration = OptionsHelper.GetConfigurationRoot();

    public static DbContextOptions<T> GetOptions<T>(string? connectionString = null, bool useRandomDatabaseIdentifier = true) where T : DbContext
    {
        return new DbContextOptionsBuilder<T>()
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .Options;
    }
}