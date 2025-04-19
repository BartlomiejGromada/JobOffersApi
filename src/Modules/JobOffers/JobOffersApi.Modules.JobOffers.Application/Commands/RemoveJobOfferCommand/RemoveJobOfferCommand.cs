using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.RemoveJobOfferCommand;

internal class RemoveJobOfferCommand : ICommand
{
    public RemoveJobOfferCommand(Guid jobOfferId)
    {
        JobOfferId = jobOfferId;
    }

    public Guid JobOfferId { get; init; }
}
