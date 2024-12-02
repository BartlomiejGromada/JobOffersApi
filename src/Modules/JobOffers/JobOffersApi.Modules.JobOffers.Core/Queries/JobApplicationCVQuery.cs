using JobOffersApi.Abstractions.Queries;

namespace JobOffersApi.Modules.JobOffers.Core.Queries;

internal class JobApplicationCVQuery : IQuery<byte[]?>
{
    public JobApplicationCVQuery(
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
