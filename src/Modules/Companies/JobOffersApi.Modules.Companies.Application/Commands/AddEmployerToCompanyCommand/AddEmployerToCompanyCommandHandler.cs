using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Exceptions;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Companies.Core.Entities;
using JobOffersApi.Modules.Companies.Core.Events;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Integration.Services;
using JobOffersApi.Modules.Users.Integration.Queries;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.Companies.Application.Commands.AddEmployerToCompanyCommand;

internal sealed class AddEmployerToCompanyCommandHandler : ICommandHandler<AddEmployerToCompanyCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IEmployersRepository _employersRepository;
    private readonly IClock _clock;
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;
    private readonly IAuthorizationCompanyService _authorizationCompanyService;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<AddEmployerToCompanyCommandHandler> _logger;

    public AddEmployerToCompanyCommandHandler(
        ICompaniesRepository companiesRepository,
        IEmployersRepository employersRepository,
        IClock clock,
        IDispatcher dispatcher,
        IContext context,
        IAuthorizationCompanyService authorizationCompanyService,
        ILogger<AddEmployerToCompanyCommandHandler> logger,
        IMessageBroker messageBroker)
    {
        _companiesRepository = companiesRepository;
        _employersRepository = employersRepository;
        _clock = clock;
        _dispatcher = dispatcher;
        _context = context;
        _authorizationCompanyService = authorizationCompanyService;
        _logger = logger;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(AddEmployerToCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var invokerId = _context.Identity.Id;
        await _authorizationCompanyService.ValidateWorkingInCompanyAsync(
           invokerId, command.CompanyId, cancellationToken);

        var user = await _dispatcher.QueryAsync(
            new UserQuery { UserId = command.UserId }, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        if (user.RoleName != Roles.Employer && user.RoleName != Roles.OwnerCompany)
        {
            throw new InvalidUserRoleException("A user with a role other than employer or owner-company cannot be added to a company.");
        }

        var employer = await _employersRepository.GetAsync(user.Id, cancellationToken);

        if (employer is null)
        {
            employer = new Employer(user.Id, user.FirstName, user.LastName,
                user.DateOfBirth, _clock.CurrentDateOffset());

            await _employersRepository.AddAsync(employer, cancellationToken);
        }

        var company = await _companiesRepository.GetAsync(command.CompanyId, cancellationToken);
        company!.AddEmployer(employer, command.Position, _clock.CurrentDateOffset());

        await _companiesRepository.UpdateAsync(company, cancellationToken);

        await _messageBroker.PublishAsync(
                new EmployerAddedToCompany(command.UserId, command.CompanyId), cancellationToken);

        _logger.LogInformation($"Employer with id: {command.UserId} " +
            $"was successfully added to company with id: {command.CompanyId}.");
    }
}
