using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Exceptions;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Companies.Core.Entities;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Core.Storages;
using JobOffersApi.Modules.Users.Integration.Queries;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.Companies.Application.Commands.Handlers;

internal sealed class AddEmployerToCompanyCommandHandler : ICommandHandler<AddEmployerToCompanyCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IEmployersRepository _employersRepository;
    private readonly IClock _clock;
    private readonly IDispatcher _dispatcher;
    private readonly ICompaniesStorage _companiesStorage;
    private readonly ILogger<AddEmployerToCompanyCommandHandler> _logger;

    public AddEmployerToCompanyCommandHandler(
        ICompaniesRepository companiesRepository,
        IEmployersRepository employersRepository,
        IClock clock,
        IDispatcher dispatcher,
        ILogger<AddEmployerToCompanyCommandHandler> logger,
        ICompaniesStorage companiesStorage)
    {
        _companiesRepository = companiesRepository;
        _employersRepository = employersRepository;
        _clock = clock;
        _dispatcher = dispatcher;
        _logger = logger;
        _companiesStorage = companiesStorage;
    }

    public async Task HandleAsync(AddEmployerToCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var company = await _companiesRepository.GetAsync(command.CompanyId, cancellationToken);

        if (company is null)
        {
            throw new CompanyNotFoundException(command.CompanyId);
        }

        if (command.InvokerRole != Roles.Admin)
        {
            var hasAccess = await _companiesStorage.IsWorkingAsync(company.Id, command.InvokerId, cancellationToken);
            if(!hasAccess)
            {
                throw new UnauthorizedCompanyAccessException(company.Id, command.InvokerId);
            }
        }

        var user = await _dispatcher.QueryAsync(
            new UserQueryByEmail { Email = command.UserEmail }, cancellationToken);

        if(user is null)
        {
            throw new UserNotFoundException(command.UserEmail);
        }

        if(user.RoleName != Roles.Employer)
        {
            throw new InvalidUserRoleException("A user with a role other than employer cannot be added to a company.");
        }

        var employer = await _employersRepository.GetAsync(user.Id, cancellationToken);

        if(employer is null)
        {
            employer = new Employer(user.Id, user.FirstName, user.LastName,
                user.DateOfBirth, _clock.CurrentDateOffset());
            await _employersRepository.AddAsync(employer, cancellationToken);
        }

        company.AddEmployer(employer, command.Position, _clock.CurrentDateOffset());

        await _companiesRepository.UpdateAsync(company, cancellationToken);

        _logger.LogInformation($"Employer with email: {command.UserEmail} " +
            $"was successfully added to company with id: {command.CompanyId}.");
    }
}
