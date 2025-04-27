using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Shared.Tests;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.EntityFrameworkCore;
using JobOffersApi.Modules.Companies.Tests.Integration.Common;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Infrastructure.DAL.Repositories;
using JobOffersApi.Modules.Companies.Application.Commands.RemoveCompanyCommand;
using JobOffersApi.Modules.Companies.Integration.Services;

namespace JobOffersApi.Modules.Companies.Tests.Integration;

public class RemoveCompanyCommandTests : IntegrationTestBase, IDisposable
{
    private TestCompaniesDbContext _dbContext;
    private ICompaniesRepository _repository;
    private IClock _clock;
    private Mock<IAuthorizationCompanyService> _authorizationCompanyServiceMock;
    private IMessageBroker _messageBroker;
    private IContext _context;
    private readonly Guid _employerId = Guid.NewGuid();

    [Fact]
    public async Task Should_Remove_Company_From_Database()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();

        var company = new Core.Entities.Company(
            "Company 1", string.Empty, _clock.CurrentDateOffset(),
            new Location("Poland", "Poznań", "Główna", "10", "5", "61-001"));
        
        company.AddEmployer(new Core.Entities.Employer(_employerId,
            string.Empty, string.Empty, DateOnly.MaxValue, _clock.CurrentDateOffset()),
            "Company owner", _clock.CurrentDateOffset());

        await _dbContext.Context.Companies.AddAsync(company);
        await _dbContext.Context.SaveChangesAsync();

        var companyFromDatabase = await _dbContext.Context.Companies.FirstAsync();

        var loggerMock = new Mock<ILogger<RemoveCompanyCommandHandler>>();

        var identityContext = new TestIdentityContext(
            _employerId, Roles.CompanyOwner, claims: null);

        _context = new TestContext(Guid.NewGuid(), string.Empty, identityContext);

        _authorizationCompanyServiceMock
          .Setup(service => service.ValidateCompanyOwnerAsync(
              It.IsAny<Guid>(),
              It.IsAny<Guid>(),
              default));

        var handler = new RemoveCompanyCommandHandler(
            _repository,
            _messageBroker,
            _authorizationCompanyServiceMock.Object,
            _context,
            loggerMock.Object);

        // Act
        await handler.HandleAsync(
            new RemoveCompanyCommand()
            {
                CompanyId = companyFromDatabase.Id,
            },
            CancellationToken.None);

        var companiesCount = await _dbContext.Context.Companies.CountAsync();

        // Assert
        Assert.Equal(0, companiesCount);
    }


    public override void Initialize()
    {
        _dbContext = new TestCompaniesDbContext(ConnectionString);
        _repository = new CompaniesRepository(_dbContext.Context);
        _authorizationCompanyServiceMock = new Mock<IAuthorizationCompanyService>();
        _clock = new TestClock();
        _messageBroker = new TestMessageBroker();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}