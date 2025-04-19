using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.JobOffers.Core.Events;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.UpdateJobOfferCommand;

internal sealed class UpdateJobOfferCommandHandler : ICommandHandler<UpdateJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IContext _context;
    private readonly ILogger<UpdateJobOfferCommandHandler> _logger;

    public UpdateJobOfferCommandHandler(
        IJobOffersRepository repository,
        IClock clock,
        IMessageBroker messageBroker,
        IAuthorizationCompanyService authorizationCompanyService,
        IContext context,
        ILogger<UpdateJobOfferCommandHandler> logger)
    {
        _repository = repository;
        _clock = clock;
        _messageBroker = messageBroker;
        _authorizationCompanyService = authorizationCompanyService;
        _context = context;
        _logger = logger;
    }

    public async Task HandleAsync(UpdateJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        var identity = _context.Identity;

        var dto = command.Dto;

        await _authorizationCompanyService.ValidateWorkingInCompanyAsync(
            identity.Id,
            command.Dto.CompanyId,
            cancellationToken);

        var jobOffer = await _repository.GetAsync(command.JobOfferId, cancellationToken);
        jobOffer!.Update(dto, _clock.CurrentDateOffset());

        await _repository.UpdateAsync(jobOffer, cancellationToken);

        await _messageBroker.PublishAsync(
            new JobOfferUpdated(identity.Id, jobOffer.Id), cancellationToken);

        _logger.LogInformation($"User with id: {identity.Id}" +
             $"was updated successfully job offer with id: {command.JobOfferId}");
    }
}
