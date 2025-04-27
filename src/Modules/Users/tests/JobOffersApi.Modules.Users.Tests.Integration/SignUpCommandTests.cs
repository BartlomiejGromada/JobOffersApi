using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Kernel;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Tests.Integration.Common;
using JobOffersApi.Modules.Users.Application.Commands.SignUpCommand;
using JobOffersApi.Modules.Users.Core;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Modules.Users.Core.Repositories;
using JobOffersApi.Modules.Users.Infrastructure.DAL.Repositories;
using JobOffersApi.Shared.Tests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace JobOffersApi.Modules.Users.Tests.Integration;

public class SignUpCommandTests : IntegrationTestBase, IDisposable
{
    private TestUsersDbContext _dbContext;
    private IUsersRepository _repository;
    private IRolesRepository _rolesRepository;
    private Mock<IPasswordHasher<User>> _passwordHasherMock;
    private IClock _clock;
    private IMessageBroker _messageBroker;
    private Mock<IDomainEventDispatcher> _domainEventDispatcherMock;

    [Fact]
    public async Task Should_Sign_Up_User()
    {
        // Arrange
        await _dbContext.Context.Database.EnsureCreatedAsync();

        var role = new Role(Roles.Employer, []);
        await _dbContext.Context.Roles.AddAsync(role);
        await _dbContext.Context.SaveChangesAsync();

        _domainEventDispatcherMock
            .Setup(dispatcher => dispatcher.DispatchAsync(
                It.IsAny<IDomainEvent[]>(),
                default));

        _passwordHasherMock.Setup(hasher => hasher.HashPassword(
            It.IsAny<User>(), "password@123!"))
            .Returns("hashed_password@123!");

        var addJobOfferCommand = new SignUpCommand(
                "user@gmail.com",
                "password@123!",
                "password@123!",
                "User",
                "User",
                Roles.Employer,
                _clock.CurrentDateOffset());
        
        var loggerMock = new Mock<ILogger<SignUpCommandHandler>>();

        var handler = new SignUpCommandHandler(
            _repository,
            _rolesRepository,
            _passwordHasherMock.Object,
            _clock,
            _messageBroker,
            new RegistrationOptions()
            {
                Enabled = true
            },
            loggerMock.Object,
            _domainEventDispatcherMock.Object);

        // Act
        await handler.HandleAsync(
            addJobOfferCommand,
            CancellationToken.None);

        var usersCount = await _dbContext.Context.Users.CountAsync();

        // Assert
        Assert.Equal(1, usersCount);
    }

    public override void Initialize()
    {
        _dbContext = new TestUsersDbContext(ConnectionString);
        _repository = new UsersRepository(_dbContext.Context);
        _rolesRepository = new RolesRepository(_dbContext.Context);
        _passwordHasherMock = new Mock<IPasswordHasher<User>>();
        _clock = new TestClock();
        _messageBroker = new TestMessageBroker();
        _domainEventDispatcherMock = new Mock<IDomainEventDispatcher>();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
