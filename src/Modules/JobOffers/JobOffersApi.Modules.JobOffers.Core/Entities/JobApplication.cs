using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;

namespace JobOffersApi.Modules.JobOffers.Core.Entities;

internal class JobApplication : Entity<Guid>
{
    private JobApplication()
    {
        // EF Core needs it
    }
    public JobApplication(
       Guid candidateId,
       string candidateFirstName,
       string candidateLastName,
       Guid jobOfferId,
       string? messageToEmployer,
       Disposition disposition,
       FinancialCondition? financialExpectations,
       ContractType? preferredContract,
       DateTimeOffset createdAt,
       byte[] cv)
    {
        CandidateId = candidateId;
        CandidateFirstName = candidateFirstName;
        CandidateLastName = candidateLastName;
        JobOfferId = jobOfferId;
        MessageToEmployer = messageToEmployer;
        Disposition = disposition;
        FinancialExpectations = financialExpectations;
        PreferredContract = preferredContract;
        Status = JobApplicationStatus.Submitted;
        CreatedAt = createdAt;
        CV = cv;
    }

    public Guid CandidateId { get; private set; }
    public string CandidateFirstName { get; private set; }
    public string CandidateLastName { get; private set; }
    public JobOffer JobOffer { get; private set; }
    public Guid JobOfferId { get; private set; }
    public string? MessageToEmployer { get; private set; }
    public Disposition Disposition { get; private set; }
    public FinancialCondition? FinancialExpectations { get; private set; }
    public ContractType? PreferredContract { get; private set; }
    public JobApplicationStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public byte[] CV { get; private set; }
}
