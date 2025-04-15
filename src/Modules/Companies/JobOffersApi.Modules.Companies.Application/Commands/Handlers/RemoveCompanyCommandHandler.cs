using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Modules.Companies.Core.Events;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Core.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.Companies.Application.Commands.Handlers;

internal sealed class RemoveCompanyCommandHandler : ICommandHandler<RemoveCompanyCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IContext _context;
    private readonly ILogger<RemoveCompanyCommandHandler> _logger;

    public RemoveCompanyCommandHandler(
        ICompaniesRepository companiesRepository,
        IMessageBroker messageBroker,
        IAuthorizationCompanyService authorizationCompanyService,
        IContext context,
        ILogger<RemoveCompanyCommandHandler> logger)
    {
        _companiesRepository = companiesRepository;
        _messageBroker = messageBroker;
        _authorizationCompanyService = authorizationCompanyService;
        _logger = logger;
        _context = context;
    }

    public async Task HandleAsync(RemoveCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var userId = _context.Identity.Id;
        var companyId = command.Id;

        var isCompanyOwner = await _authorizationCompanyService.IsCompanyOwnerAsync(
            userId,
            companyId,
            cancellationToken);

        if (isCompanyOwner)
        {
            throw new NotCompanyOwnerException(userId, companyId);
        }

        var company = await _companiesRepository.GetAsync(companyId, cancellationToken)!;

        await _companiesRepository.RemoveAsync(company!, cancellationToken);

        await _messageBroker.PublishAsync(
            new CompanyRemoved(userId, companyId), cancellationToken);

        _logger.LogInformation($"Company with id: {companyId} was successfully removed.");
    }
}
