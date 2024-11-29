using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Helpers;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Entities.Extensions;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Modules.Users.Integration.DTO;
using JobOffersApi.Modules.Users.Integration.Exceptions;
using JobOffersApi.Modules.Users.Integration.Queries;
using JobOffersApi.Modules.Users.Integration.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Core.Commands.Handlers;

internal sealed class ApplyForJobOfferCommandHandler : ICommandHandler<ApplyForJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IFileHelper _fileHelper;
    private readonly IDispatcher _dispatcher;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly IUserValidationService _userValidationService;
    private readonly ILogger<ApplyForJobOfferCommandHandler> _logger;

    public ApplyForJobOfferCommandHandler(
        IJobOffersRepository repository,
        IFileHelper fileHelper,
        IDispatcher dispatcher,
        IClock clock,
        IMessageBroker messageBroker,
        IUserValidationService userValidationService,
        ILogger<ApplyForJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _fileHelper = fileHelper;
        _dispatcher = dispatcher;
        _clock = clock;
        _messageBroker = messageBroker;
        _userValidationService = userValidationService;
        _logger = logger;
    }

    public async Task HandleAsync(ApplyForJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var cvBytes = await _fileHelper.ConvertToByteArrayAsync(
            command.Dto.CV, cancellationToken);

        var user = await _userValidationService.ValidateAsync(command.CandidateId, cancellationToken);
        var jobOffer = await ValidateJobOfferAsync(command.JobOfferId, cancellationToken);

        var jobApplication = CreateJobApplication(
            command, 
            user,
            cvBytes);

        jobOffer.ApplyForJob(jobApplication);

        await _repository.UpdateAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(
            new AppliedForJob(command.CandidateId, command.JobOfferId), cancellationToken);

        _logger.LogInformation($"User with id: {command.CandidateId}" +
            $"was applied successfully for job offer with id: {command.JobOfferId}");
    }

    private async Task<JobOffer> ValidateJobOfferAsync(
        Guid jobOfferId, 
        CancellationToken cancellationToken)
    {
        var jobOffer = await _repository.GetAsync(jobOfferId, cancellationToken);

        if (jobOffer is null)
        {
            throw new JobOfferNotFoundException(jobOfferId);
        }

        return jobOffer;
    }

    private JobApplication CreateJobApplication(
        ApplyForJobOfferCommand command,
        UserDto user, 
        byte[] cvBytes)
    {
        var dto = command.Dto;

        var disposition = new Entities.ValueObjects.Disposition(
            dto.Availability,
            dto.AvailabilityDate is not null
                ? DateOnly.FromDateTime(dto.AvailabilityDate.Value.DateTime)
                : null);

        var financialCondition = dto.FinancialExpectations is not null
            ? dto.FinancialExpectations.ToValueObject()
            : null;

        return new JobApplication(
            command.CandidateId,
            user.FirstName,
            user.LastName,
            command.JobOfferId,
            dto.MessageToEmployer,
            disposition,
            financialCondition,
            dto.PreferredContract,
            _clock.CurrentDateOffset(),
            cvBytes);
    }
}
