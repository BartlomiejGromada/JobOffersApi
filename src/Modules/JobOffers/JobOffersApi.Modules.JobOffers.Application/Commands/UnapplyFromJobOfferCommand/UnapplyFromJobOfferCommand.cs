using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.UnapplyFromJobOfferCommand;

internal class UnapplyFromJobOfferCommand : ICommand
{
    public UnapplyFromJobOfferCommand(Guid jobOfferId, Guid jobApplicationId, Guid invokerId)
    {
        JobOfferId = jobOfferId;
        JobApplicationId = jobApplicationId;
        InvokerId = invokerId;
    }

    public Guid JobOfferId { get; init; }
    public Guid JobApplicationId { get; init; }
    public Guid InvokerId { get; init; }
}
