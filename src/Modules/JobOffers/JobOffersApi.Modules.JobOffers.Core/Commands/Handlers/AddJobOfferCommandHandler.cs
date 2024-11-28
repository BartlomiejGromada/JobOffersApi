using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.Time;
using JobOffersApi.Modules.JobOffers.Core.Entities;
using JobOffersApi.Modules.JobOffers.Core.Repositories;

namespace JobOffersApi.Modules.JobOffers.Core.Commands;

internal class AddJobOfferCommandHandler : ICommandHandler<AddJobOfferCommand>
{
    private readonly IJobOffersRepository _repository;
    private readonly IClock _clock;

    public AddJobOfferCommandHandler(IJobOffersRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task HandleAsync(AddJobOfferCommand command, CancellationToken cancellationToken = default)
    {
        // TODO: company pobierać z modułu firmy i sprawdzić czy user ma dostęp do tej firmy

        var jobOffer = new JobOffer(
            command.Title,
            command.DescriptionHtml,
            command.Location,
            command.FinancialCondition,
            _clock.CurrentDate(),
            command.CompanyId,
            command.CompanyName,
            command.Attributes,
            command.ValidityInDays);

        await _repository.AddAsync(jobOffer, cancellationToken);
    }
}
