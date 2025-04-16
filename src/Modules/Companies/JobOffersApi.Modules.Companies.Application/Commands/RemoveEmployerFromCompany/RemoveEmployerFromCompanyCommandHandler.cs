using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Exceptions;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Companies.Core.Events;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.Users.Integration.Queries;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.Companies.Application.Commands.RemoveEmployerFromCompany;

internal sealed class RemoveEmployerFromCompanyCommandHandler
    : ICommandHandler<RemoveEmployerFromCompanyCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IDispatcher _dispatcher;
    private readonly IMessageBroker _messageBroker;
    private readonly IContext _context;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly ILogger<RemoveEmployerFromCompanyCommandHandler> _logger;

    public RemoveEmployerFromCompanyCommandHandler(
        ICompaniesRepository companiesRepository,
        IClock clock,
        IDispatcher dispatcher,
        IMessageBroker messageBroker,
        IContext context,
        IAuthorizationCompanyService authorizationCompanyService,
        ILogger<RemoveEmployerFromCompanyCommandHandler> logger)
    {
        _companiesRepository = companiesRepository;
        _dispatcher = dispatcher;
        _messageBroker = messageBroker;
        _context = context;
        _authorizationCompanyService = authorizationCompanyService;
        _logger = logger;
    }

    public async Task HandleAsync(RemoveEmployerFromCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var invokerId = _context.Identity.Id;
        await _authorizationCompanyService.ValidateWorkingInCompanyAsync(
            invokerId, command.CompanyId, cancellationToken);

        var user = await _dispatcher.QueryAsync(
                 new UserQuery { UserId = command.EmployerId }, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(command.EmployerId);
        }

        if (user.RoleName != Roles.Employer)
        {
            throw new InvalidUserRoleException("A user with a role other than employer cannot be removed from a company.");
        }

        var company = await _companiesRepository.GetAsync(command.CompanyId, cancellationToken);

        company!.RemoveEmployer(command.EmployerId);

        await _companiesRepository.UpdateAsync(company, cancellationToken);

        await _messageBroker.PublishAsync(
                new EmployerRemovedFromCompany(command.EmployerId, command.CompanyId), cancellationToken);

        _logger.LogInformation($"Employer with id: {command.EmployerId} " +
            $"was successfully removed from company with id: {command.CompanyId}.");
    }
}
