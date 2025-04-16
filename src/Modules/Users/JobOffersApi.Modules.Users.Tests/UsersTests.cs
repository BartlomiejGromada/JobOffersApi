using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Modules.Users.Core.Exceptions;

namespace JobOffersApi.Modules.Users.Tests.Unit;

public class UsersTests
{
    [Fact]
    public void Should_Block_User_End_With_Success()
    {
        // Arrange
        var user = new User(
            string.Empty,
            "12121@@2323ddfd",
            "Jan",
            "Kowalski",
            new Role(Roles.Candidate, new List<string>()),
            DateOnly.MinValue,
            DateTimeOffset.Now);

        // Act
        user.SetState(UserState.Locked);

        // Assert
        Assert.True(user.IsLocked);
    }

    [Fact]
    public void Should_UnBlock_User_End_With_Success()
    {
        // Arrange
        var user = new User(
            string.Empty,
            "12121@@2323ddfd",
            "Jan",
            "Kowalski",
            new Role(Roles.Candidate, new List<string>()),
            DateOnly.MinValue,
            DateTimeOffset.Now);

        user.SetState(UserState.Locked);

        // Act
        user.SetState(UserState.Active);

        // Assert
        Assert.False(user.IsLocked);
    }

    [Fact]
    public void Should_Change_User_State_Throw_Exception()
    {
        // Arrange
        var user = new User(
            string.Empty,
            "12121@@2323ddfd",
            "Jan",
            "Kowalski",
            new Role(Roles.Admin, new List<string>()),
            DateOnly.MinValue,
            DateTimeOffset.Now);

        // Act & Assert
        var exception = Assert.Throws<UserStateCannotBeChangedException>(() =>
          user.SetState(UserState.Active)
      );
    }
}