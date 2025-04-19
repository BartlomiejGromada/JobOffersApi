using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.ChangeStatusOfJobApplicationCommand;

internal class ChangeStatusOfJobApplicationCommand : ICommand
{
    public Guid JobOfferId { get; init; }
    public Guid JobApplicationId { get; init; }
    public JobApplicationStatus Status { get; init; }
}
