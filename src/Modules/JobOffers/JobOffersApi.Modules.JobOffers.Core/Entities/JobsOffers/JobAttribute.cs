using JobOffersApi.Abstractions.Core;

namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobsOffers;

internal class JobAttribute : Entity<Guid>
{
    public JobAttribute(JobAttributeType type, string name)
    {
        Type = type;
        Name = name;
    }

    private JobAttribute() { }

    public JobAttributeType Type { get; private set; }
    public string Name { get; private set; }
}