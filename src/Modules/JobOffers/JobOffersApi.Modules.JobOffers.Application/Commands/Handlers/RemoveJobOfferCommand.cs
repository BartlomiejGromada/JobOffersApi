using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Modules.JobOffers.Application.Services;
using JobOffersApi.Modules.JobOffers.Core.Events;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.Users.Integration.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.Handlers;

internal sealed class RemoveJobOfferCommandHandler : ICommandHandler<RemoveJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IUsersService _usersService;
    private readonly IMessageBroker _messageBroker;
    private readonly IJobOffersService _jobOffersService;
    private readonly ILogger<RemoveJobOfferCommandHandler> _logger;

    public RemoveJobOfferCommandHandler(
        IJobOffersRepository repository,
        IUsersService usersService,
        IMessageBroker messageBroker,
        IJobOffersService jobOffersService,
        ILogger<RemoveJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _usersService = usersService;
        _messageBroker = messageBroker;
        _jobOffersService = jobOffersService;
        _logger = logger;
    }

    public async Task HandleAsync(RemoveJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var userDto = await _usersService.GetAsync(command.InvokerId, cancellationToken);

        await _jobOffersService.ValidateAccessAsync(
            command.JobOfferId,
            command.InvokerId,
            command.InvokerRole,
            cancellationToken);

        var jobOffer = await _repository.GetAsync(command.JobOfferId, cancellationToken);

        await _repository.RemoveAsync(jobOffer, cancellationToken);
        await _messageBroker.PublishAsync(
            new JobOffeRemoved(command.InvokerId, jobOffer.Id), cancellationToken);

        _logger.LogInformation($"User with id: {command.InvokerId}" +
             $"was removed successfully job offer with id: {command.JobOfferId}");
    }
}
