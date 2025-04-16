using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Modules.Companies.Core.Events;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Integration.Services;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.Companies.Application.Commands.UpdateCompanyCommand;

internal sealed class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IContext _context;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly ILogger<UpdateCompanyCommandHandler> _logger;

    public UpdateCompanyCommandHandler(
        ICompaniesRepository companiesRepository,
        IMessageBroker messageBroker,
        IContext context,
        IAuthorizationCompanyService authorizationCompanyService,
        ILogger<UpdateCompanyCommandHandler> logger)
    {
        _companiesRepository = companiesRepository;
        _messageBroker = messageBroker;
        _context = context;
        _authorizationCompanyService = authorizationCompanyService;
        _logger = logger;
    }

    public async Task HandleAsync(UpdateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var userId = _context.Identity.Id;
        var companyId = command.CompanyId;
        var updateDto = command.Dto;

        await _authorizationCompanyService.ValidateCompanyOwnerAsync(
         userId,
         companyId,
         cancellationToken);

        var company = await _companiesRepository.GetAsync(companyId, cancellationToken)!;

        company!.Update(
            updateDto.Name,
            updateDto.Description,
            updateDto.Location);

        await _companiesRepository.UpdateAsync(company, cancellationToken);

        await _messageBroker.PublishAsync(
          new CompanyUpdated(userId, companyId), cancellationToken);

        _logger.LogInformation($"Company with id: {companyId} was successfully updated.");

    }
}
