using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.Companies.Core.Entities;
using JobOffersApi.Modules.Companies.Core.Events;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using JobOffersApi.Modules.Companies.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace JobOffersApi.Modules.Companies.Application.Commands.AddCompanyCommand;

internal sealed class AddCompanyCommandHandler : ICommandHandler<AddCompanyCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IEmployersRepository _employersRepository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly IContext _context;
    private readonly ILogger<AddCompanyCommandHandler> _logger;

    public AddCompanyCommandHandler(
        ICompaniesRepository companiesRepository,
        IEmployersRepository employersRepository,
        IClock clock,
        IMessageBroker messageBroker,
        IContext context,
        ILogger<AddCompanyCommandHandler> logger)
    {
        _companiesRepository = companiesRepository;
        _employersRepository = employersRepository;
        _clock = clock;
        _messageBroker = messageBroker;
        _context = context;
        _logger = logger;
    }

    public async Task HandleAsync(AddCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var userId = _context.Identity.Id;
        var userRole = _context.Identity.Role;
        var locationDto = command.Location;

        if (userRole != Roles.OwnerCompany)
        {
            throw new NotCompanyOwnerException(userId);
        }

        var company = new Company(command.Name, command.Description, _clock.CurrentDateOffset(),
            new Location(locationDto.Country,
                        locationDto.City,
                        locationDto.HouseNumber,
                        locationDto.Street,
                        locationDto.ApartmentNumber,
                        locationDto.PostalCode));

        var employer = await _employersRepository.GetAsync(userId, cancellationToken);
        if (employer == null)
        {
            throw new UserNotFoundException(userId);
        }

        company.AddEmployer(employer, userRole, _clock.CurrentDateOffset());
        await _companiesRepository.AddAsync(company, cancellationToken);

        await _messageBroker.PublishAsync(
            new CompanyAdded(userId, company.Id), cancellationToken);

        _logger.LogInformation($"Company with name: {command.Name} was successfully added.");
    }
}
