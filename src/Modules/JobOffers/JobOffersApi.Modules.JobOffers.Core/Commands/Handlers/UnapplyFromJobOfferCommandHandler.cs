using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Modules.Users.Integration.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Core.Commands.Handlers;

internal sealed class UnapplyFromJobOfferCommandHandler : ICommandHandler<UnapplyFromJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IMessageBroker _messageBroker;
    private readonly IUsersService _userValidationService;
    private readonly ILogger<UnapplyFromJobOfferCommandHandler> _logger;

    public UnapplyFromJobOfferCommandHandler(
        IJobOffersRepository repository,
        IMessageBroker messageBroker,
        IUsersService userValidationService,
        ILogger<UnapplyFromJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _messageBroker = messageBroker;
        _userValidationService = userValidationService;
        _logger = logger;
    }

    public async Task HandleAsync(UnapplyFromJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userValidationService.GetAsync(command.InvokerId, cancellationToken);

        var jobOffer = await _repository.GetAsync(command.JobOfferId, cancellationToken);

        var jobApplication = jobOffer!.JobApplications.FirstOrDefault(ja => ja.Id == command.JobApplicationId);

        if (jobApplication is null)
        {
            throw new JobApplicationNotFoundException(command.JobApplicationId);
        }

        if (jobApplication.CandidateId != command.InvokerId)
        {
            throw new InvalidAccessToJobApplicationException(command.JobApplicationId, command.InvokerId);
        }

        jobOffer.UnapplyFromJob(jobApplication);

        await _repository.UpdateAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(
            new UnappliedFromJob(command.InvokerId, command.JobOfferId), cancellationToken);

        _logger.LogInformation($"User with id: {command.InvokerId}" +
            $"unapplied successfully from job offer with id: {command.JobOfferId}");
    }
}
