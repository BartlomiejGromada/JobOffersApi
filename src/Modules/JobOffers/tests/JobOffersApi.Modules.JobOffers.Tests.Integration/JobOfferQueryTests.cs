using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobOfferQuery;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Entities.Enums;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;
using JobOffersApi.Modules.JobOffers.Core.Storages;
using JobOffersApi.Modules.JobOffers.Infrastructure.Storages;
using JobOffersApi.Modules.JobOffers.Tests.Integration.Common;
using JobOffersApi.Shared.Tests;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.JobOffers.Tests.Integration;

public class JobOfferQueryTests : IntegrationTestBase, IDisposable
{
    private TestJobOffersDbContext _dbContext;
    private IClock _clock;
    private IJobOffersStorage _storage;

    [Fact]
    public async Task Should_Return_Job_Offer_From_Database()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();

        await _dbContext.Context.JobOffers.AddAsync(
            new JobOffer(
                "Software Engineer",
                "<p>Join our team and work on exciting projects!</p>",
                new Location("Poland", "Warsaw", "Main Street", "123", "00-001"),
                createdDate: DateTimeOffset.UtcNow,
                companyId: Guid.NewGuid(),
                companyName: "Tech Innovations",
                attributes:
                [
                    new JobAttribute(JobAttributeType.EmploymentType, "Full-time"),
                    new JobAttribute(JobAttributeType.ExperienceLevel, "Mid")
                ],
                conditions:
                [
                    new FinancialCondition(12000m, CurrencyCode.PLN, SalaryType.Netto, SalaryPeriod.PerMonth)
                ],
                60));
        await _dbContext.Context.SaveChangesAsync();

        var jobOffer = await _dbContext.Context.JobOffers.FirstAsync();

        var handler = new JobOfferQueryHandler(_storage);

        // Act
        var jobOfferDto = await handler.HandleAsync(
            new JobOfferQuery(jobOffer.Id),
            CancellationToken.None);

        // Assert
        Assert.Equal(jobOfferDto!.Id, jobOfferDto.Id);
    }


    [Fact]
    public async Task Should_Return_Null_From_Database()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();

        var handler = new JobOfferQueryHandler(_storage);

        // Act
        var jobOfferDto = await handler.HandleAsync(
            new JobOfferQuery(Guid.NewGuid()),
            CancellationToken.None);

        // Assert
        Assert.Null(jobOfferDto);
    }


    public override void Initialize()
    {
        _dbContext = new TestJobOffersDbContext(ConnectionString);
        _clock = new TestClock();
        _storage = new JobOffersStorage(_dbContext.Context, _clock);
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
