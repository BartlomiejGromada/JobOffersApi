using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.JobOffers.Core.DTO.Extensions;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Modules.Users.Integration.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Core.Commands;

internal class AddJobOfferCommandHandler : ICommandHandler<AddJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IClock _clock;
    private readonly IUsersService _usersService;
    private readonly IMessageBroker _messageBroker;
    private readonly ICompaniesService _companiesService;
    private readonly ILogger<AddJobOfferCommandHandler> _logger;

    public AddJobOfferCommandHandler(
        IJobOffersRepository repository,
        IClock clock,
        IUsersService usersService,
        IMessageBroker messageBroker,
        ICompaniesService companiesService,
        ILogger<AddJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _clock = clock;
        _usersService = usersService;
        _messageBroker = messageBroker;
        _companiesService = companiesService;
        _logger = logger;
    }

    public async Task HandleAsync(AddJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var userDto = await _usersService.GetAsync(
            command.EmployerId,
            cancellationToken);

        await _companiesService.HasAccessAsync(
            command.Dto.CompanyId,
            command.EmployerId,
            cancellationToken);

        var dto = command.Dto;

        var jobOffer = dto.ToEntity(_clock.CurrentDateOffset());

        await _repository.AddAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(new JobOfferAdded(jobOffer.Id), cancellationToken);

        _logger.LogInformation($"User with id: {command.EmployerId}" +
             $"was added successfully job offer for company with id: {dto.CompanyId}");
    }
}
