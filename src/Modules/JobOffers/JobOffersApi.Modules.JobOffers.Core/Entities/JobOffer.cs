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
    private List<FinancialCondition> financialConditions = new();

    private JobOffer()
    {
        // EF Core needs it
    }

    public JobOffer(
        string title,
        string descriptionHtml,
        Location location,
        DateTimeOffset createdDate,
        Guid companyId,
        string companyName,
        List<JobAttribute> attributes,
        List<FinancialCondition>? conditions = null,
        int? validityInDays = null)
    {
        Title = title;
        DescriptionHtml = descriptionHtml;
        Location = location;
        CreatedDate = createdDate;
        ExpirationDate = CreatedDate.AddDays(validityInDays ?? DefaultValidityInDays);
        CompanyId = companyId;
        CompanyName = companyName;
        financialConditions = conditions ?? new List<FinancialCondition>();
        jobAttributes = attributes;
    }

    public string Title { get; private set; }
    public string DescriptionHtml { get; private set; }
    public Location Location { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public DateTimeOffset ExpirationDate { get; private set; }
    public Guid CompanyId { get; private set; }
    public string CompanyName { get; private set; }

    public IReadOnlyCollection<JobApplication> JobApplications => jobApplications;
    public IReadOnlyCollection<JobAttribute> JobAttributes => jobAttributes;
    public IReadOnlyCollection<FinancialCondition> FinancialConditions => financialConditions;

    public bool UserAlreadyApplied(Guid userId) 
        => JobApplications.Any(ja => ja.CandidateId == userId);

    public void ApplyForJob(JobApplication jobApplication)
    {
        var candidateId = jobApplication.CandidateId;

        if(UserAlreadyApplied(candidateId))
        {
            throw new UserAlreadyAppliedForJobException(candidateId, Id);
        }

        var appliedAt = jobApplication.CreatedAt;

        if(ExpirationDate < appliedAt) 
        {
            throw new JobOfferExpiredException(ExpirationDate);
        }

        jobApplications.Add(jobApplication);
    }

}
