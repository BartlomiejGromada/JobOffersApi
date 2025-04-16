using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.JobOffers.Core.DTO.Extensions;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using JobOffersApi.Modules.Users.Core.Events;
using JobOffersApi.Modules.Users.Integration.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.AddJobOfferCommand;

internal class AddJobOfferCommandHandler : ICommandHandler<AddJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IClock _clock;
    private readonly IUsersService _usersService;
    private readonly IMessageBroker _messageBroker;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IContext _context;
    private readonly ILogger<AddJobOfferCommandHandler> _logger;

    public AddJobOfferCommandHandler(
        IJobOffersRepository repository,
        IClock clock,
        IUsersService usersService,
        IMessageBroker messageBroker,
        IAuthorizationCompanyService authorizationCompanyService,
        IContext context,
        ILogger<AddJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _clock = clock;
        _usersService = usersService;
        _messageBroker = messageBroker;
        _authorizationCompanyService = authorizationCompanyService;
        _context = context;
        _logger = logger;
    }

    public async Task HandleAsync(AddJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;

        var userDto = await _usersService.GetAsync(
            command.EmployerId,
            cancellationToken);

        await _authorizationCompanyService.ValidateWorkingInCompanyAsync(
            identity.Id,
            command.Dto.CompanyId,
            cancellationToken);

        var dto = command.Dto;

        var jobOffer = dto.ToEntity(_clock.CurrentDateOffset());

        await _repository.AddAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(new JobOfferAdded(jobOffer.Id), cancellationToken);

        _logger.LogInformation($"User with id: {command.EmployerId}" +
             $"was added successfully job offer for company with id: {dto.CompanyId}");
    }
}
