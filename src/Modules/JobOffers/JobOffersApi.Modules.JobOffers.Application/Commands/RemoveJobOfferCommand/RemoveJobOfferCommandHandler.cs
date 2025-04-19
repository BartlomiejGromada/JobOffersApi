using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Modules.JobOffers.Core.Events;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.JobOffers.Core.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.RemoveJobOfferCommand;

internal sealed class RemoveJobOfferCommandHandler : ICommandHandler<RemoveJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IMessageBroker _messageBroker;
    private readonly IAuthorizationJobOfferService _authorizationJobOfferService;
    private readonly IContext _context;
    private readonly ILogger<RemoveJobOfferCommandHandler> _logger;

    public RemoveJobOfferCommandHandler(
        IJobOffersRepository repository,
        IMessageBroker messageBroker,
        IAuthorizationJobOfferService authorizationJobOfferService,
        IContext context,
        ILogger<RemoveJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _messageBroker = messageBroker;
        _authorizationJobOfferService = authorizationJobOfferService;
        _context = context;
        _logger = logger;
    }

    public async Task HandleAsync(RemoveJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;

        await _authorizationJobOfferService.ValidateAccessToJobOffer(
            command.JobOfferId,
            cancellationToken);

        var jobOffer = await _repository.GetAsync(command.JobOfferId, cancellationToken);

        await _repository.RemoveAsync(jobOffer!, cancellationToken);
        await _messageBroker.PublishAsync(
            new JobOffeRemoved(identity.Id, jobOffer!.Id), cancellationToken);

        _logger.LogInformation($"User with id: {identity.Id}" +
             $"was removed successfully job offer with id: {command.JobOfferId}");
    }
}
