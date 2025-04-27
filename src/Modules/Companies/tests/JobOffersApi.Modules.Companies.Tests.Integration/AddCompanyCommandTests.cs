using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Shared.Tests;
using Microsoft.Extensions.Logging;
using Moq;
using JobOffersApi.Abstractions.Dispatchers;
using Microsoft.EntityFrameworkCore;
using JobOffersApi.Modules.Companies.Tests.Integration.Common;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Infrastructure.DAL.Repositories;
using JobOffersApi.Modules.Companies.Application.Commands.AddCompanyCommand;
using JobOffersApi.Abstractions.DTO;

namespace JobOffersApi.Modules.Companies.Tests.Integration;

public class AddCompanyCommandTests : IntegrationTestBase, IDisposable
{
    private TestCompaniesDbContext _dbContext;
    private ICompaniesRepository _repository;
    private IEmployersRepository _employersRepository;
    private IClock _clock;
    private Mock<IDispatcher> _dispatcherMock;
    private IMessageBroker _messageBroker;
    private IContext _context;
    private readonly Guid _employerId = Guid.NewGuid();

    [Fact]
    public async Task Should_Add_Company_To_Database()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();

        await _dbContext.Context.Employers.AddAsync(
            new Core.Entities.Employer(_employerId,
            string.Empty, string.Empty, DateOnly.MaxValue, _clock.CurrentDateOffset()));
        await _dbContext.Context.SaveChangesAsync();

        var addCompanyCommand = new AddCompanyCommand()
        {
            Name = "Company 1",
            Description = string.Empty,
            Location = new AddLocationDto
            {
                Country = "Poland",
                City = "Poznań",
                Street = "Główna",
                HouseNumber = "10",
                ApartmentNumber = "5",
                PostalCode = "61-001"
            },
        };

        var loggerMock = new Mock<ILogger<AddCompanyCommandHandler>>();

        var identityContext = new TestIdentityContext(
            _employerId, Roles.CompanyOwner, claims: null);

        _context = new TestContext(Guid.NewGuid(), string.Empty, identityContext);

        var handler = new AddCompanyCommandHandler(
            _repository,
            _employersRepository,
            _clock,
            _messageBroker,
            _context,
            _dispatcherMock.Object,
            loggerMock.Object);

        // Act
        await handler.HandleAsync(
            addCompanyCommand,
            CancellationToken.None);

        var companyFromDatabase = await _dbContext.Context.Companies.FirstAsync();

        // Assert
        Assert.Equal(companyFromDatabase.Name, addCompanyCommand.Name);
    }


    public override void Initialize()
    {
        _dbContext = new TestCompaniesDbContext(ConnectionString);
        _repository = new CompaniesRepository(_dbContext.Context);
        _employersRepository = new EmployersRepository(_dbContext.Context);
        _clock = new TestClock();
        _messageBroker = new TestMessageBroker();
        _dispatcherMock = new Mock<IDispatcher>();

    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}