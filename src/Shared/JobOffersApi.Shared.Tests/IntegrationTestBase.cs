using Newtonsoft.Json;
using Testcontainers.MsSql;
using Xunit;

namespace JobOffersApi.Shared.Tests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected TestMessageBroker MessageBroker = new TestMessageBroker();
    protected string ConnectionString => _mssqlSqlContainer.GetConnectionString();
    private readonly MsSqlContainer _mssqlSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public async Task InitializeAsync()
    {
        await _mssqlSqlContainer.StartAsync();
        Initialize();
    }

    public abstract void Initialize();

    public async Task DisposeAsync()
    {
        await _mssqlSqlContainer.DisposeAsync();
    }
}

public static class ObjectExtensions
{
    public static T Clone<T>(this T source)
    {
        // Don't serialize a null object, simply return the default for that object
        if (source == null)
        {
            return default;
        }

        // initialize inner objects individually
        // for example in default constructor some list property initialized with some values,
        // but in 'source' these items are cleaned -
        // without ObjectCreationHandling.Replace default constructor values will be added to result
        var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
    }
}