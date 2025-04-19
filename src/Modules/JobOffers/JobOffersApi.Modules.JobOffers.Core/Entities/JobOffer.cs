using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.DTO.Extensions;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Entities;

internal class JobOffer : AggregateRoot<Guid>
{
    private const int DefaultValidityInDays = 30;

    private List<JobApplication.JobApplication> jobApplications = new();
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

    public IReadOnlyCollection<JobApplication.JobApplication> JobApplications => jobApplications;
    public IReadOnlyCollection<JobAttribute> JobAttributes => jobAttributes;
    public IReadOnlyCollection<FinancialCondition> FinancialConditions => financialConditions;

    public bool UserAlreadyApplied(Guid userId) 
        => JobApplications.Any(ja => ja.CandidateId == userId);

    public bool UserDidNotApply(Guid userId) => !UserAlreadyApplied(userId);

    public bool IsExpired(DateTimeOffset now) => ExpirationDate < now;

    public void ApplyForJob(JobApplication.JobApplication jobApplication)
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

    public void UnapplyFromJob(JobApplication.JobApplication jobApplication, DateTimeOffset withdrawDate)
    {
        var candidateId = jobApplication.CandidateId;

        if (UserDidNotApply(candidateId))
        {
            throw new UserDidNotApplyForJobException(candidateId, Id);
        }

        if (ExpirationDate < withdrawDate)
        {
            throw new JobOfferExpiredException(ExpirationDate);
        }

        jobApplication.Withdraw();
    }

    public void Update(UpdateJobOfferDto dto, DateTimeOffset updateDate)
    {
        if (ExpirationDate < updateDate)
        {
            throw new JobOfferExpiredException(ExpirationDate);
        }

        Title = dto.Title;
        DescriptionHtml = dto.DescriptionHtml;
        Location = dto.Location.ToValueObject();
        ExpirationDate = CreatedDate.AddDays(dto.ValidityInDays ?? DefaultValidityInDays);
        CompanyId = dto.CompanyId;
        CompanyName = dto.CompanyName;

        financialConditions.Clear();
        financialConditions = dto.FinancialConditions?.Select(f => f.ToValueObject()).ToList();

        jobApplications.Clear();
        jobAttributes = dto.Attributes.Select(a => a.ToEntity()).ToList();
    }
}
