using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Tests.Unit;

public sealed class JobOffersTests
{
    public JobOffersTests()
    {
    }

    [Fact]
    public void Should_Throw_Exception_When_User_Already_Applied_For_Job()
    {
        // Arrange
        var jobOffer = new JobOffer(
            "Praca 1",
            string.Empty,
            new Location("Poland", "Poznań", "10"),
            DateTimeOffset.UtcNow,
            Guid.NewGuid(),
            "Comapny",
            new List<JobAttribute>());

        var jobApplication = new JobApplication(
            Guid.NewGuid(),
            "Jan",
            "Kowalski",
            Guid.NewGuid(),
            string.Empty,
            new Core.Entities.ValueObjects.Disposition(
                Availability.InTwoWeek, null),
            null,
            null,
            DateTimeOffset.UtcNow,
            new byte[1]);

        jobOffer.ApplyForJob(jobApplication);

        // Act & Assert
        var exception = Assert.Throws<UserAlreadyAppliedForJobException>(() =>
            jobOffer.ApplyForJob(jobApplication)
        );
    }

    [Fact]
    public void Should_Throw_Exception_When_Job_Offer_Expired()
    {
        // Arrange
        var jobOffer = new JobOffer(
            "Praca 1",
            string.Empty,
            new Location("Poland", "Poznań", "10"),
            DateTimeOffset.UtcNow.AddDays(-10),
            Guid.NewGuid(),
            "Comapny",
            new List<JobAttribute>(),
            null,
            3);


        var jobApplication = new JobApplication(
            Guid.NewGuid(),
            "Jan",
            "Kowalski",
            Guid.NewGuid(),
            string.Empty,
            new Core.Entities.ValueObjects.Disposition(
                Availability.InTwoWeek, null),
            null,
            null,
            DateTimeOffset.UtcNow,
            new byte[1]);

        // Act & Assert
        var exception = Assert.Throws<JobOfferExpiredException>(() =>
            jobOffer.ApplyForJob(jobApplication)
        );
    }

    [Fact]
    public void Should_End_With_Success_When_User_Applied_For_Job()
    {
        // Arrange
        var jobOffer = new JobOffer(
            "Praca 1",
            string.Empty,
            new Location("Poland", "Poznań", "10"),
            DateTimeOffset.UtcNow,
            Guid.NewGuid(),
            "Comapny",
            new List<JobAttribute>());

        var jobApplication = new JobApplication(
            Guid.NewGuid(),
            "Jan",
            "Kowalski",
            Guid.NewGuid(),
            string.Empty,
            new Core.Entities.ValueObjects.Disposition(
                Availability.InTwoWeek, null),
            null,
            null,
            DateTimeOffset.UtcNow,
            new byte[1]);

        // Act
        var exception = Record.Exception(() => jobOffer.ApplyForJob(jobApplication));

        // Assert
        Assert.Null(exception);
        Assert.Equal(1, jobOffer.JobApplications.Count);
    }
}
