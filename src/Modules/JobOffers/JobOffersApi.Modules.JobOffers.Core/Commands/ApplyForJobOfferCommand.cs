using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.JobOffers.Core.DTO;

namespace JobOffersApi.Modules.JobOffers.Core.Commands;

internal class ApplyForJobOfferCommand : ICommand
{
    public Guid JobOfferId { get; init; }
    public ApplyForJobDto Dto { get; init; }
    public Guid CandidateId { get; init; }
}