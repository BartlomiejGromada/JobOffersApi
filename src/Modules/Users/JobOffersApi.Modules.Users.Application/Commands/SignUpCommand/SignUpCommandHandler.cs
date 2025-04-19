using JobOffersApi.Abstractions;
using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Kernel;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Users.Core;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Modules.Users.Core.Exceptions;
using JobOffersApi.Modules.Users.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.Users.Application.Commands.SignUpCommand;

internal sealed class SignUpCommandHandler : ICommandHandler<SignUpCommand>
{
    private readonly IUsersRepository _userRepository;
    private readonly IRolesRepository _roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly RegistrationOptions _registrationOptions;
    private readonly ILogger<SignUpCommandHandler> _logger;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public SignUpCommandHandler(
        IUsersRepository userRepository,
        IRolesRepository roleRepository,
        IPasswordHasher<User> passwordHasher,
        IClock clock,
        IMessageBroker messageBroker,
        RegistrationOptions registrationOptions,
        ILogger<SignUpCommandHandler> logger,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _clock = clock;
        _messageBroker = messageBroker;
        _registrationOptions = registrationOptions;
        _logger = logger;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task HandleAsync(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        if (!_registrationOptions.Enabled)
        {
            throw new SignUpDisabledException();
        }

        var email = command.Email.ToLowerInvariant();
        var provider = email.Split("@").Last();
        if (_registrationOptions.InvalidEmailProviders?.Any(x => provider.Contains(x)) is true)
        {
            throw new InvalidEmailException(email);
        }

        var user = await _userRepository.GetAsync(email);
        if (user is not null)
        {
            throw new EmailInUseException();
        }

        var role = await _roleRepository.GetAsync(command.Role)
            .NotNull(() => new RoleNotFoundException(command.Role));

        var now = _clock.CurrentDate();
        var password = _passwordHasher.HashPassword(user: default, command.Password);

        user = new User(
            command.Email,
            password,
            command.FirstName,
            command.LastName,
            role,
            DateOnly.Parse(command.DateOfBirth.ToString("dd-MM-yyyy")),
            now);

        await _userRepository.AddAsync(user);
        await _domainEventDispatcher.DispatchAsync(user.GetDomainEvents().ToArray(), cancellationToken);

        await _messageBroker.PublishAsync(new SignedUp(user.Id, email, role.Name), cancellationToken);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
    }
}