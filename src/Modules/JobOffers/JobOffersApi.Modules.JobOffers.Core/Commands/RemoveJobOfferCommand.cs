using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.JobOffers.Core.Commands;

internal class RemoveJobOfferCommand : ICommand
{
    public RemoveJobOfferCommand(Guid jobOfferId, Guid invokerId, string invokerRole)
    {
        JobOfferId = jobOfferId;
        InvokerId = invokerId;
        InvokerRole = invokerRole;
    }

    public Guid JobOfferId { get; init; }
    public Guid InvokerId { get; init; }
    public string InvokerRole { get; init; }
}
