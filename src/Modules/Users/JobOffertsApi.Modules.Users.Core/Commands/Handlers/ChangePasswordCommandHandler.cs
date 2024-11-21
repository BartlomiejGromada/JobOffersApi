using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Modules.Users.Core.Exceptions;
using JobOffersApi.Modules.Users.Core.Repositories;
using JobOffersApi.Abstractions;
using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Core.Commands.Handlers;

internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly IUsersRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;

    public ChangePasswordCommandHandler(
        IUsersRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        ILogger<ChangePasswordCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task HandleAsync(ChangePasswordCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(command.Email)
            .NotNull(() => new UserNotFoundException(command.Email));

        if (user.IsLocked)
        {
            throw new UserNotActiveException(user.Id);
        }

        var isValidPassword = _passwordHasher.VerifyHashedPassword(default, user.HashedPassword, command.CurrentPassword) ==
            PasswordVerificationResult.Success;

        if (!isValidPassword)
        {
            throw new InvalidPasswordException("Current password is invalid");
        }

        user.SetHashedPassword(_passwordHasher.HashPassword(default, command.NewPassword));
        await _userRepository.UpdateAsync(user);
        _logger.LogInformation($"User with ID: '{user.Id}' has changed password.");
    }
}