using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.ApplyForJobOfferCommand;

internal class ApplyForJobOfferCommand : ICommand
{
    public Guid JobOfferId { get; init; }
    public AddJobApplicationDto Dto { get; init; }
}