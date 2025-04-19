using JobOffersApi.Abstractions.Events;
using JobOffersApi.Modules.Companies.Integration.Events;
using JobOffersApi.Modules.JobOffers.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.JobOffers.Application.Events.Externals;

internal sealed class CompanyRemovedHandler : IEventHandler<CompanyRemoved>
{
    private readonly IJobOffersRepository _repository;
    private readonly ILogger<CompanyRemovedHandler> _logger;

    public CompanyRemovedHandler(
        IJobOffersRepository repository,
        ILogger<CompanyRemovedHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task HandleAsync(CompanyRemoved @event, CancellationToken cancellationToken = default)
    {
        var jobOffers = await _repository.GetByCompanyIdAsync(@event.CompanyId, cancellationToken);

        await _repository.RemoveAsync(jobOffers, cancellationToken);

        _logger.LogInformation($"Job offers for company with id: {@event.CompanyId} was successfully removed.");
    }
}
