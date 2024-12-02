using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Modules.Users.Core.Exceptions;

namespace JobOffersApi.Modules.Users.Tests.Unit;

public class UsersTests
{
    [Fact]
    public void Should_Block_User_End_With_Success()
    {
        // Act
        var user = new User(
            string.Empty,
            "12121@@2323ddfd",
            "Jan",
            "Kowalski",
            new Role(Roles.Candidate, new List<string>()),
            DateOnly.MinValue,
            DateTimeOffset.Now);

        // Arrange
        user.SetState(UserState.Locked);

        // Assert
        Assert.True(user.IsLocked);
    }

    [Fact]
    public void Should_UnBlock_User_End_With_Success()
    {
        // Act
        var user = new User(
            string.Empty,
            "12121@@2323ddfd",
            "Jan",
            "Kowalski",
            new Role(Roles.Candidate, new List<string>()),
            DateOnly.MinValue,
            DateTimeOffset.Now);

        user.SetState(UserState.Locked);

        // Arrange
        user.SetState(UserState.Active);

        // Assert
        Assert.False(user.IsLocked);
    }

    [Fact]
    public void Should_Change_User_State_Throw_Exception()
    {
        // Act
        var user = new User(
            string.Empty,
            "12121@@2323ddfd",
            "Jan",
            "Kowalski",
            new Role(Roles.Admin, new List<string>()),
            DateOnly.MinValue,
            DateTimeOffset.Now);

        // Arrange & Assert
        var exception = Assert.Throws<UserStateCannotBeChangedException>(() =>
          user.SetState(UserState.Active)
      );
    }
}