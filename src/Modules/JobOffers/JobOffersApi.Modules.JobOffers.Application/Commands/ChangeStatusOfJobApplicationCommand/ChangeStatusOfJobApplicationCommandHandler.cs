using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Core.Events;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.JobOffers.Core.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.ChangeStatusOfJobApplicationCommand;

internal sealed class ChangeStatusOfJobApplicationCommandHandler : ICommandHandler<ChangeStatusOfJobApplicationCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IMessageBroker _messageBroker;
    private readonly IContext _context;
    private readonly IAuthorizationJobApplicationService _authorizationJobApplicationService;
    private readonly IClock _clock;
    private readonly ILogger<ChangeStatusOfJobApplicationCommandHandler> _logger;

    public ChangeStatusOfJobApplicationCommandHandler(
        IJobOffersRepository repository,
        IMessageBroker messageBroker,
        IContext context,
        IAuthorizationJobApplicationService authorizationJobApplicationService,
        IClock clock,
        ILogger<ChangeStatusOfJobApplicationCommandHandler> logger)
    {
        _repository = repository;
        _messageBroker = messageBroker;
        _context = context;
        _authorizationJobApplicationService = authorizationJobApplicationService;
        _clock = clock;
        _logger = logger;
    }

    public async Task HandleAsync(ChangeStatusOfJobApplicationCommand command, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;

        await _authorizationJobApplicationService.ValidateAccessToJobApplication(
            command.JobOfferId, command.JobApplicationId, cancellationToken);

        var jobOffer = await _repository.GetWithJobApplicationsAsync(command.JobOfferId, cancellationToken);

        if(jobOffer!.IsExpired(_clock.CurrentDateOffset()))
        {
            throw new JobOfferExpiredException(jobOffer.ExpirationDate);
        }

        var jobApplication = jobOffer!.JobApplications.First(ja => ja.Id == command.JobApplicationId);
        jobApplication.RestoreStateFromStatus();

        ChangeStatusOfJobApplication(jobApplication, command.Status);

        await _repository.UpdateAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(
            new ChangedJobApplicationStatus(identity.Id, command.JobOfferId, command.Status), cancellationToken);

        _logger.LogInformation($"User with id: {identity.Id}" +
            $" changed job application status with id: {command.JobOfferId} on {command.Status} successfully.");
    }

    private void ChangeStatusOfJobApplication(
        JobApplication jobApplication,
        JobApplicationStatus newStatus)
    {
        switch (newStatus)
        {
            case JobApplicationStatus.Applied:
                jobApplication.Apply(); break;
            case JobApplicationStatus.InReview:
                jobApplication.Review(); break;
            case JobApplicationStatus.InterviewScheduled:
                jobApplication.ScheduleInterview(); break;
            case JobApplicationStatus.Interviewed:
                jobApplication.ConductInterview(); break;
            case JobApplicationStatus.OfferReceived:
                jobApplication.ReceiveOffer(); break;
            case JobApplicationStatus.Rejected:
                jobApplication.Reject(); break;
            case JobApplicationStatus.OfferAccepted:
                jobApplication.AcceptOffer(); break;
            case JobApplicationStatus.Withdrawn:
                jobApplication.Withdraw(); break;
            default:
                throw new InvalidOperationException();
        }
    }
}
