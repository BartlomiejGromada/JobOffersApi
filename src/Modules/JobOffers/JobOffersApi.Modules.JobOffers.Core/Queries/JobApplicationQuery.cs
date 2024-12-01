using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

namespace JobOffersApi.Modules.JobOffers.Core.Queries;

internal class JobApplicationQuery : IQuery<JobApplicationDto?>
{
    public JobApplicationQuery(Guid jobOfferId, Guid jobApplicationId)
    {
        JobOfferId = jobOfferId;
        JobApplicationId = jobApplicationId;
    }

    public Guid JobOfferId { get; init; }
    public Guid JobApplicationId { get; init; }
}