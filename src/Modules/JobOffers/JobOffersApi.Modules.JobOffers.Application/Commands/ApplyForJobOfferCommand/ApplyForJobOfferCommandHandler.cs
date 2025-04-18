﻿using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Helpers;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Core.DTO.Extensions;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Modules.Users.Integration.DTO;
using JobOffersApi.Modules.Users.Integration.Queries;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.ApplyForJobOfferCommand;

internal sealed class ApplyForJobOfferCommandHandler : ICommandHandler<ApplyForJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IFileHelper _fileHelper;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly IContext _context;
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<ApplyForJobOfferCommandHandler> _logger;

    public ApplyForJobOfferCommandHandler(
        IJobOffersRepository repository,
        IFileHelper fileHelper,
        IClock clock,
        IMessageBroker messageBroker,
        IContext context,
        IDispatcher dispatcher,
        ILogger<ApplyForJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _fileHelper = fileHelper;
        _clock = clock;
        _messageBroker = messageBroker;
        _context = context;
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public async Task HandleAsync(ApplyForJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;

        var jobOffer = await _repository.GetWithJobApplicationsAsync(command.JobOfferId, cancellationToken);

        if (jobOffer is null)
        {
            throw new JobOfferNotFoundException(command.JobOfferId);
        }

        var cvBytes = await _fileHelper.ConvertToByteArrayAsync(
            command.Dto.CV, cancellationToken);

        var user = await _dispatcher.QueryAsync(new UserQuery { UserId = identity.Id }, cancellationToken);

        var jobApplication = CreateJobApplication(
            command,
            user!,
            cvBytes);

        jobOffer.ApplyForJob(jobApplication);

        await _repository.UpdateAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(
            new AppliedForJob(identity.Id, command.JobOfferId), cancellationToken);

        _logger.LogInformation($"User with id: {identity.Id}" +
            $"was applied successfully for job offer with id: {command.JobOfferId}");
    }

    private JobApplication CreateJobApplication(
        ApplyForJobOfferCommand command,
        UserDto user,
        byte[] cvBytes)
    {
        var dto = command.Dto;

        var disposition = new Core.Entities.ValueObjects.Disposition(
            dto.Availability,
            dto.AvailabilityDate is not null
                ? DateOnly.FromDateTime(dto.AvailabilityDate.Value.DateTime)
                : null);

        var financialCondition = dto.FinancialExpectations is not null
            ? dto.FinancialExpectations.ToValueObject()
            : null;

        return new JobApplication(
            _context.Identity.Id,
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
