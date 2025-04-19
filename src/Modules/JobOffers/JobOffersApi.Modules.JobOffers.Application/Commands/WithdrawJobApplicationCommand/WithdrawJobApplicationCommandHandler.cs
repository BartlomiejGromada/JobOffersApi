using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.JobOffers.Core.Services;
using JobOffersApi.Modules.Users.Core.Events;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.WithdrawJobApplicationCommand;

internal sealed class WithdrawJobApplicationCommandHandler : ICommandHandler<WithdrawJobApplicationCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IMessageBroker _messageBroker;
    private readonly IContext _context;
    private readonly IAuthorizationJobApplicationService _authorizationJobApplicationService;
    private readonly IClock _clock;
    private readonly ILogger<WithdrawJobApplicationCommandHandler> _logger;

    public WithdrawJobApplicationCommandHandler(
        IJobOffersRepository repository,
        IMessageBroker messageBroker,
        IContext context,
        IAuthorizationJobApplicationService authorizationJobApplicationService,
        IClock clock,
        ILogger<WithdrawJobApplicationCommandHandler> logger)
    {
        _repository = repository;
        _messageBroker = messageBroker;
        _context = context;
        _authorizationJobApplicationService = authorizationJobApplicationService;
        _clock = clock;
        _logger = logger;
    }

    public async Task HandleAsync(WithdrawJobApplicationCommand command, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;

        await _authorizationJobApplicationService.ValidateAccessToJobApplication(
            command.JobOfferId, command.JobApplicationId, cancellationToken);

        var jobOffer = await _repository.GetWithJobApplicationsAsync(command.JobOfferId, cancellationToken);

        var jobApplication = jobOffer!.JobApplications.First(ja => ja.Id == command.JobApplicationId);
        jobApplication.RestoreStateFromStatus();

        jobOffer.UnapplyFromJob(jobApplication, _clock.CurrentDateOffset());

        await _repository.UpdateAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(
            new UnappliedFromJob(identity.Id, command.JobOfferId, command.JobApplicationId), cancellationToken);

        _logger.LogInformation($"User with id: {identity.Id}" +
            $" withdrew job application with id: {command.JobOfferId} successfully.");
    }
}
