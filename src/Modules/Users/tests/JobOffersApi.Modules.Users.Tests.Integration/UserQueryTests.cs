using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Tests.Integration.Common;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Shared.Tests;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Modules.Users.Infrastructure.Storages;
using JobOffersApi.Modules.Users.Integration.Queries;
using JobOffersApi.Modules.Users.Application.Queries.UserQuery;

namespace JobOffersApi.Modules.Users.Tests.Integration;

public class UserQueryTests : IntegrationTestBase, IDisposable
{
    private TestUsersDbContext _dbContext;
    private IUsersStorage _storage;
    private IClock _clock;

    [Fact]
    public async Task Should_Return_User_From_Database()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();
        var role = new Role(Roles.Employer, []);
        await _dbContext.Context.Roles.AddAsync(role);
        await _dbContext.Context.SaveChangesAsync();

        var user = new User("user@gmail.com", "password", "user", "user",
            role, DateOnly.MinValue, _clock.CurrentDateOffset());
        await _dbContext.Context.Users.AddAsync(user);
        await _dbContext.Context.SaveChangesAsync();

        var userQuery = new UserQueryByEmail()
        {
            Email = user.Email
        };

        var handler = new UserQueryByEmailHandler(_storage);

        // Act
        var userDto = await handler.HandleAsync(
            userQuery,
            CancellationToken.None);

        // Assert
        Assert.Equal(user.Id, userDto.Id);
    }

    public override void Initialize()
    {
        _dbContext = new TestUsersDbContext(ConnectionString);
        _storage = new UsersStorage(_dbContext.Context);
        _clock = new TestClock();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}

