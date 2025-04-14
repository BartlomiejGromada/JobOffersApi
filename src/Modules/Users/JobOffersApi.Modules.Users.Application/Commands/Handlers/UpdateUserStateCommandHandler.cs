using Microsoft.Extensions.Logging;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Modules.Users.Core.Repositories;
using JobOffersApi.Abstractions;
using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Modules.Users.Integration.Exceptions;
using JobOffersApi.Modules.Users.Application.Commands;

namespace JobOffersApi.Modules.Users.Core.Commands.Handlers;

internal sealed class UpdateUserStateCommandHandler : ICommandHandler<UpdateUserStateCommand>
{
    private readonly IUsersRepository _userRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<UpdateUserStateCommandHandler> _logger;

    public UpdateUserStateCommandHandler(IUsersRepository userRepository, IMessageBroker messageBroker,
        ILogger<UpdateUserStateCommandHandler> logger)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(UpdateUserStateCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(command.UserId)
            .NotNull(() => new UserNotFoundException(command.UserId));

        var previousState = user.State;
        if (previousState == command.State)
        {
            return;
        }
        
        user.SetState(command.State);

        await _userRepository.UpdateAsync(user);
        await _messageBroker.PublishAsync(new UserStateUpdated(user.Id, command.State.ToString().ToLowerInvariant()));
        _logger.LogInformation($"Updated state for user with ID: '{user.Id}', '{previousState}' -> '{user.State}'.");
    }
}