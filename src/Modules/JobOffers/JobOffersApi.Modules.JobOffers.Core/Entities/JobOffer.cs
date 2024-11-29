using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobOffersApi.Modules.JobOffers.Core.Entities;

internal class JobOffer : AggregateRoot<Guid>
{
    private const int DefaultValidityInDays = 30;

    private List<JobApplication> jobApplications = new();
    private List<JobAttribute> jobAttributes = new();

    private JobOffer()
    {
        // EF Core needs it
    }

    public JobOffer(
        string title,
        string descriptionHtml,
        Location location,
        FinancialCondition? financialCondition,
        DateTimeOffset createdAt,
        Guid companyId,
        string companyName,
        List<JobAttribute> attributes,
        int? validityInDays = null)
    {
        Title = title;
        DescriptionHtml = descriptionHtml;
        Location = location;
        FinancialCondition = financialCondition;
        CreatedAt = createdAt;
        CompanyId = companyId;
        CompanyName = companyName;
        jobAttributes = attributes;
        ValidityInDays = validityInDays ?? DefaultValidityInDays;
    }

    public string Title { get; private set; }
    public string DescriptionHtml { get; private set; }
    public Location Location { get; private set; }
    public FinancialCondition? FinancialCondition { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public int ValidityInDays { get; private set; }
    public Guid CompanyId { get; private set; }
    public string CompanyName { get; private set; }

    public IReadOnlyCollection<JobApplication> JobApplications => jobApplications;
    public IReadOnlyCollection<JobAttribute> JobAttributes => jobAttributes;


    [NotMapped]
    public DateTimeOffset EndDate => CreatedAt.AddDays(ValidityInDays);

    public bool UserAlreadyApplied(Guid userId) 
        => JobApplications.Any(ja => ja.CandidateId == userId);

    public void ApplyForJob(JobApplication jobApplication)
    {
        var candidateId = jobApplication.CandidateId;

        if(UserAlreadyApplied(candidateId))
        {
            throw new UserAlreadyAppliedForJobException(candidateId, Id);
        }

        var jobOfferEndDate = EndDate;
        var appliedAt = jobApplication.CreatedAt;

        if(jobOfferEndDate < appliedAt) 
        {
            throw new JobOfferExpiredException(jobOfferEndDate);
        }

        jobApplications.Add(jobApplication);
    }

}
