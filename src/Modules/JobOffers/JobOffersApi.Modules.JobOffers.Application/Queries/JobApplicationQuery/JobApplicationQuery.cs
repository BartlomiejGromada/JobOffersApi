using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

namespace JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationQuery;

internal class JobApplicationQuery : IQuery<JobApplicationDto?>
{
    public JobApplicationQuery(
        Guid jobOfferId,
        Guid jobApplicationId,
        Guid invokerId,
        string invokerRole)
    {
        JobOfferId = jobOfferId;
        JobApplicationId = jobApplicationId;
        InvokerId = invokerId;
        InvokerRole = invokerRole;
    }

    public Guid JobOfferId { get; init; }
    public Guid JobApplicationId { get; init; }
    public Guid InvokerId { get; init; }
    public string InvokerRole { get; init; }
}