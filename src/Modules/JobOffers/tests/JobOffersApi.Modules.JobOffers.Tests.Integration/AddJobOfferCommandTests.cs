using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.DTO;
using JobOffersApi.Modules.JobOffers.Application.Commands.AddJobOfferCommand;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.DTO;
using JobOffersApi.Modules.JobOffers.Core.Entities.Enums;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Tests.Integration.Common;
using JobOffersApi.Shared.Tests;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.JobOffers.Infrastructure.DAL.Repositories;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Modules.Companies.Integration.Services;
using Moq;
using JobOffersApi.Abstractions.Contexts;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.JobOffers.Tests.Integration;

public class AddJobOfferCommandTests : IntegrationTestBase, IDisposable
{
    private TestJobOffersDbContext _dbContext;
    private IJobOffersRepository _repository;
    private IClock _clock;
    private IMessageBroker _messageBroker;
    private Mock<IAuthorizationCompanyService> _authorizeServiceMock;
    private IContext _context;

    [Fact]
    public async Task Should_Add_Job_Offer_To_Database()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();

        _authorizeServiceMock
            .Setup(service => service.ValidateWorkingInCompanyAsync(
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                default));

        var addJobOfferCommand = new AddJobOfferCommand()
        {
           Dto = new AddJobOfferDto
           {
               Title = "Junior Fullstack Developer",
               DescriptionHtml = "<p>We are looking for a passionate developer to join our team!</p>",
               Location = new AddLocationDto
               {
                   Country = "Poland",
                   City = "Poznań",
                   Street = "Główna",
                   HouseNumber = "10",
                   ApartmentNumber = "5",
                   PostalCode = "61-001"
               },
               CompanyId = Guid.NewGuid(),
               CompanyName = "Tech Solutions",
               Attributes = [
                    new AddJobAttribute
                    {
                        Type = JobAttributeType.ExperienceLevel,
                        Name = "Junior"
                    },
                    new AddJobAttribute
                    {
                        Type = JobAttributeType.EmploymentType,
                        Name = "Full-time"
                    }
                ],
               FinancialConditions = [
                    new AddFinancialExpectationsDto
                    {
                        Value = 7000m,
                        ConcurrencyCode = CurrencyCode.PLN,
                        SalaryType = SalaryType.Netto,
                        SalaryPeriod = SalaryPeriod.PerMonth
                    }
                ],
               ValidityInDays = 30
           }
        };

        var loggerMock = new Mock<ILogger<AddJobOfferCommandHandler>>();

        var handler = new AddJobOfferCommandHandler(
            _repository,
            _clock,
            _messageBroker,
            _authorizeServiceMock.Object,
            _context,
            loggerMock.Object);

        // Act
        await handler.HandleAsync(
            addJobOfferCommand,
            CancellationToken.None);

        var jobOffersCount = await _dbContext.Context.JobOffers.CountAsync();

        // Assert
        Assert.Equal(1, jobOffersCount);
    }

    public override void Initialize()
    {
        _dbContext = new TestJobOffersDbContext(ConnectionString);
        _repository = new JobOffersRepository(_dbContext.Context);
        _clock = new TestClock();
        _messageBroker = new TestMessageBroker();
        _authorizeServiceMock = new Mock<IAuthorizationCompanyService>();
        _context = new TestContext();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
