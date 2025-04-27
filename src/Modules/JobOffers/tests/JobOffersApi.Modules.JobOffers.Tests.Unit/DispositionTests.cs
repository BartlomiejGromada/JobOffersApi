using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Tests.Unit;

public sealed class DispositionTests
{
    public DispositionTests()
    {
    }

    [Fact]
    public void Should_Create_Object_Successfully()
    {
        // Arrange & Act
        var exception = Record.Exception(() => new Disposition(Core.Entities.JobMenus.Availability.InOneWeek, null));

        var disposition = new Disposition(Core.Entities.JobMenus.Availability.InOneWeek, null);

        // Assert
        Assert.Null(exception);
        Assert.NotNull(disposition);
    }

    [Fact]
    public void Should_Throw_Invalid_Availability_Exception()
    {
        // Arrange & Act & Assert
        Assert.Throws<InvalidAvailabilityException>(() =>
             new Disposition(Core.Entities.JobMenus.Availability.InOneWeek,
             DateOnly.MinValue));
    }

    [Fact]
    public void Should_Throw_Invalid_Availability_Date_Exception()
    {
        // Arrange
        var date = DateTime.Now.AddDays(-10);

        // Act & Assert
        Assert.Throws<InvalidAvailabilityDateException>(() =>
             new Disposition(Core.Entities.JobMenus.Availability.PickedDate,
             DateOnly.FromDateTime(date)));
    }
}
