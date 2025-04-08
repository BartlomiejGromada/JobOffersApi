using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Modules.Users.Core.Exceptions;
using JobOffersApi.Modules.Users.Core.Repositories;
using JobOffersApi.Abstractions;
using JobOffersApi.Abstractions.Auth;
using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Abstractions.Messaging;

namespace JobOffersApi.Modules.Users.Core.Commands.Handlers;

internal sealed class SignInCommandHandler : ICommandHandler<SignInCommand>
{
    private readonly IUsersRepository _userRepository;
    private readonly IAuthManager _authManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRequestStorage _userRequestStorage;
    private readonly ILogger<SignInCommandHandler> _logger;
    private readonly IDispatcher _dispatcher;
    private readonly IMessageBroker _messageBroker;

    public SignInCommandHandler(
        IUsersRepository userRepository, 
        IAuthManager authManager,
        IPasswordHasher<User> passwordHasher,
        IUserRequestStorage userRequestStorage,
        ILogger<SignInCommandHandler> logger,
        IDispatcher dispatcher,
        IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _authManager = authManager;
        _passwordHasher = passwordHasher;
        _userRequestStorage = userRequestStorage;
        _logger = logger;
        _dispatcher = dispatcher;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(command.Email.ToLowerInvariant())
            .NotNull(() => new InvalidCredentialsException());

        bool isValidPassword = _passwordHasher.VerifyHashedPassword
            (user: default,
            user.HashedPassword, 
            command.Password) == PasswordVerificationResult.Success;

        if (!isValidPassword)
        {
            throw new InvalidCredentialsException();
        }

        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Role.Permissions,
        };

        var jwt = _authManager.CreateToken(
            user.Id, 
            user.Role.Name,
            claims: claims);

        jwt.FirstName = user.FirstName;
        jwt.LastName = user.LastName;
        jwt.DateOfBirth = user.DateOfBirth;
        jwt.Email = user.Email;

        _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        _userRequestStorage.SetToken(command.Id, jwt);

        // await _dispatcher.PublishAsync(new SignedIn(user.Id), cancellationToken); -> natychmiastowe wysłanie IEvent do IEventHandler
        await _messageBroker.PublishAsync(new SignedIn(user.Id), cancellationToken);
    }
}