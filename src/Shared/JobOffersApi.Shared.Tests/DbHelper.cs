using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobOffersApi.Shared.Tests;

public static class DbHelper
{
    private static readonly IConfiguration Configuration = OptionsHelper.GetConfigurationRoot();
    private const string MsSqlServerConfiguration = $"{JobOffersApi.Infrastructure.MsSqlServer.Extensions.MsSqlServer}:connectionString";

    public static DbContextOptions<T> GetOptions<T>() where T : DbContext
        => new DbContextOptionsBuilder<T>()
            .UseSqlServer(Configuration[MsSqlServerConfiguration])
            .EnableSensitiveDataLogging()
            .Options;
}