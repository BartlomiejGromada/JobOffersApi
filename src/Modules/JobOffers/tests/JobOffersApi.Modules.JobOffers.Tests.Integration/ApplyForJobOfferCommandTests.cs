using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Core.DTO;
using JobOffersApi.Modules.JobOffers.Core.Entities.Enums;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.JobOffers.Infrastructure.DAL.Repositories;
using JobOffersApi.Modules.JobOffers.Tests.Integration.Common;
using JobOffersApi.Shared.Tests;
using Microsoft.Extensions.Logging;
using Moq;
using JobOffersApi.Abstractions.Helpers;
using JobOffersApi.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Http;
using JobOffersApi.Modules.Users.Integration.Queries;
using JobOffersApi.Modules.JobOffers.Application.Commands.ApplyForJobOfferCommand;
using Microsoft.EntityFrameworkCore;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;
using Humanizer;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Tests.Integration;

public class ApplyForJobOfferCommandTests : IntegrationTestBase, IDisposable
{
    private TestJobOffersDbContext _dbContext;
    private IJobOffersRepository _repository;
    private Mock<IFileHelper> _fileHelperMock;
    private IClock _clock;
    private IMessageBroker _messageBroker;
    private Mock<IDispatcher> _dispatcherMock;
    private IContext _context;

    [Fact]
    public async Task Should_Add_Job_Applications_To_Database()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();

        await _dbContext.Context.JobOffers.AddAsync(
            new JobOffer(
                "Software Engineer",
                "<p>Join our team and work on exciting projects!</p>",
                1,
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

        _fileHelperMock
            .Setup(fileHelper => fileHelper.ConvertToByteArrayAsync(
                It.IsAny<IFormFile>(),
                default))
            .ReturnsAsync(System.Text.Encoding.UTF8.GetBytes("Hello World"));

        _dispatcherMock
            .Setup(dispatcher => dispatcher.QueryAsync(
                   It.IsAny<UserQuery>(), default))
            .ReturnsAsync(new Users.Integration.DTO.UserDto()
            {
                Id = Guid.NewGuid(),
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(1995, 5, 15),
                RoleName = "User",
                IsLocked = false,
                CreatedAt = DateTimeOffset.UtcNow
            });

        var applyForJobOfferCommand = new ApplyForJobOfferCommand()
        {
            JobOfferId = jobOffer.Id,
            Dto = new Core.DTO.JobApplications.AddJobApplicationDto()
            {
                MessageToEmployer = "I'm very excited about this opportunity and believe my skills are a great match!",
                Availability = Availability.Immediately,
                AvailabilityDate = null,
                FinancialExpectations = new AddFinancialExpectationsDto
                {
                    Value = 8000m,
                    ConcurrencyCode = CurrencyCode.PLN,
                    SalaryType = SalaryType.Netto,
                    SalaryPeriod = SalaryPeriod.PerMonth
                },
                PreferredContract = ContractType.Mandate,
                CV = It.IsAny<IFormFile>()
            }
        };

        var loggerMock = new Mock<ILogger<ApplyForJobOfferCommandHandler>>();

        var handler = new ApplyForJobOfferCommandHandler(
            _repository,
            _fileHelperMock.Object,
            _clock,
            _messageBroker,
            _context,
            _dispatcherMock.Object,
            loggerMock.Object);

        // Act
        await handler.HandleAsync(
            applyForJobOfferCommand,
            CancellationToken.None);

        var jobApplicationsCount = await _dbContext.Context.JobApplications.CountAsync();

        // Assert
        Assert.Equal(1, jobApplicationsCount);
    }

    [Fact]
    public async Task Should_Throw_JobOfferNotFoundException__Adding_Job_Applications_To_Database()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();

        _fileHelperMock
            .Setup(fileHelper => fileHelper.ConvertToByteArrayAsync(
                It.IsAny<IFormFile>(),
                default))
            .ReturnsAsync(System.Text.Encoding.UTF8.GetBytes("Hello World"));

        _dispatcherMock
            .Setup(dispatcher => dispatcher.QueryAsync(
                   It.IsAny<UserQuery>(), default))
            .ReturnsAsync(new Users.Integration.DTO.UserDto()
            {
                Id = Guid.NewGuid(),
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateOnly(1995, 5, 15),
                RoleName = "User",
                IsLocked = false,
                CreatedAt = DateTimeOffset.UtcNow
            });

        var applyForJobOfferCommand = new ApplyForJobOfferCommand()
        {
            JobOfferId = Guid.Empty,
            Dto = new Core.DTO.JobApplications.AddJobApplicationDto()
            {
                MessageToEmployer = "I'm very excited about this opportunity and believe my skills are a great match!",
                Availability = Availability.Immediately,
                AvailabilityDate = null,
                FinancialExpectations = new AddFinancialExpectationsDto
                {
                    Value = 8000m,
                    ConcurrencyCode = CurrencyCode.PLN,
                    SalaryType = SalaryType.Netto,
                    SalaryPeriod = SalaryPeriod.PerMonth
                },
                PreferredContract = ContractType.Mandate,
                CV = It.IsAny<IFormFile>()
            }
        };

        var loggerMock = new Mock<ILogger<ApplyForJobOfferCommandHandler>>();

        var handler = new ApplyForJobOfferCommandHandler(
            _repository,
            _fileHelperMock.Object,
            _clock,
            _messageBroker,
            _context,
            _dispatcherMock.Object,
            loggerMock.Object);

        // Act && Assert
        var exception = await Assert.ThrowsAsync<JobOfferNotFoundException>(
            () => handler.HandleAsync(applyForJobOfferCommand,
                CancellationToken.None));
    }


    public override void Initialize()
    {
        _dbContext = new TestJobOffersDbContext(ConnectionString);
        _repository = new JobOffersRepository(_dbContext.Context);
        _fileHelperMock = new Mock<IFileHelper>();
        _clock = new TestClock();
        _messageBroker = new TestMessageBroker();
        _dispatcherMock = new Mock<IDispatcher>();
        _context = new TestContext();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
