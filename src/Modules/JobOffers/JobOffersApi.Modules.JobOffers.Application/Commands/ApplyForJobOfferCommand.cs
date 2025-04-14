using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

namespace JobOffersApi.Modules.JobOffers.Application.Commands;

internal class ApplyForJobOfferCommand : ICommand
{
    public Guid JobOfferId { get; init; }
    public AddJobApplicationDto Dto { get; init; }
    public Guid CandidateId { get; init; }
}