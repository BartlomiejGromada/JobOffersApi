using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Entities.Extensions;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Modules.Users.Integration.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Core.Commands;

internal class AddJobOfferCommandHandler : ICommandHandler<AddJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IDispatcher _dispatcher;
    private readonly IClock _clock;
    private readonly IUserValidationService _userValidationService;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<AddJobOfferCommandHandler> _logger;

    public AddJobOfferCommandHandler(
        IJobOffersRepository repository,
        IDispatcher dispatcher,
        IClock clock,
        IUserValidationService userValidationService,
        IMessageBroker messageBroker,
        ILogger<AddJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _dispatcher = dispatcher;
        _clock = clock;
        _userValidationService = userValidationService;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(AddJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userValidationService.ValidateAsync(command.EmployerId, cancellationToken);

        var dto = command.Dto;
        // TODO: company pobierać z modułu firmy i sprawdzić czy user ma dostęp do tej firmy

        var financailCondition = dto.FinancialCondition is not null ? 
            dto.FinancialCondition.ToValueObject() : null;

        var jobOffer = new JobOffer(
            dto.Title,
            dto.DescriptionHtml,
            dto.Location,
            financailCondition,
            _clock.CurrentDate(),
            dto.CompanyId,
            dto.CompanyName,
            dto.Attributes,
            dto.ValidityInDays);

        await _repository.AddAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(new JobOfferAdded(jobOffer.Id), cancellationToken);

        _logger.LogInformation($"User with id: {command.EmployerId}" +
             $"was added successfully job offer for company with id: {dto.CompanyId}");
    }
}
