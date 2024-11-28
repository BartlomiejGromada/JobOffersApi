using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobOffersApi.Modules.JobOffers.Core.Entities;

internal class JobAttribute : Entity<Guid>
{
    private List<JobOffer> jobOffers;

    public JobAttribute(JobAttributeType type, string name)
    {
        Type = type;
        Name = name;
    }

    private JobAttribute() { }

    public JobAttributeType Type { get; private set; }
    public string Name { get; private set; }

    [NotMapped]
    public List<JobOffer> JobOffers => jobOffers;
}