using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Exceptions;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using JobOffersApi.Modules.Companies.Core.Repositories;
using JobOffersApi.Modules.Companies.Core.Storages;
using JobOffersApi.Modules.Users.Integration.Queries;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.Companies.Core.Commands.Handlers;

internal sealed class RemoveEmployerFromCompanyCommandHandler 
    : ICommandHandler<RemoveEmployerFromCompanyCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IEmployersStorage _employersStorage;
    private readonly ICompaniesStorage _companiesStorage;
    private readonly ILogger<RemoveEmployerFromCompanyCommandHandler> _logger;

    public RemoveEmployerFromCompanyCommandHandler(
        ICompaniesRepository companiesRepository,
        IEmployersStorage employersStorage,
        IClock clock,
        ICompaniesStorage companiesStorage,
        ILogger<RemoveEmployerFromCompanyCommandHandler> logger)
    {
        _companiesRepository = companiesRepository;
        _employersStorage = employersStorage;
        _companiesStorage = companiesStorage;
        _logger = logger;
    }

    public async Task HandleAsync(RemoveEmployerFromCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var company = await _companiesRepository.GetAsync(command.CompanyId, cancellationToken);

        if (company is null)
        {
            throw new CompanyNotFoundException(command.CompanyId);
        }

        if (command.InvokerRole != Roles.Admin)
        {
            var hasAccess = await _companiesStorage.IsWorkingAsync(company.Id, command.InvokerId, cancellationToken);
            if (!hasAccess)
            {
                throw new UnauthorizedCompanyAccessException(company.Id, command.InvokerId);
            }
        }

        var employer = await _employersStorage.GetByIdAsync(command.EmployerId, cancellationToken);

        if (employer is null)
        {
            throw new UserNotFoundException(command.EmployerId);
        }

        company.RemoveEmployer(command.EmployerId);

        await _companiesRepository.UpdateAsync(company, cancellationToken);

        _logger.LogInformation($"Employer with id: {employer.Id} " +
            $"was successfully removed from company with id: {command.CompanyId}.");
    }
}
