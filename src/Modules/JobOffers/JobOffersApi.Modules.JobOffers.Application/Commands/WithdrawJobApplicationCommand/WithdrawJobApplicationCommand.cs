using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.WithdrawJobApplicationCommand;

internal class WithdrawJobApplicationCommand : ICommand
{
    public Guid JobOfferId { get; init; }
    public Guid JobApplicationId { get; init; }
}
