using JobOffersApi.Abstractions.Queries;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationCVQuery;

internal class JobApplicationCVQuery : IQuery<byte[]?>
{
    public JobApplicationCVQuery(
        Guid jobOfferId,
        Guid jobApplicationId)
    {
        JobOfferId = jobOfferId;
        JobApplicationId = jobApplicationId;
    }

    public Guid JobOfferId { get; init; }
    public Guid JobApplicationId { get; init; }
}
