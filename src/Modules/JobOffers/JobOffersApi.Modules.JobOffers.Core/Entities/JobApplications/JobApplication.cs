using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobsOffers;

namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplications;

internal class JobApplication : Entity<Guid>
{
    public JobApplication(
        Guid candidateId,
        string candidateFirstName, 
        string candidateLastName,
        JobOffer jobOffer, 
        Guid jobOfferId, 
        string? messageToEmployer,
        Disposition disposition, 
        FinancialExpectations? financialExpectations, 
        ContractType? preferredContract, 
        JobApplicationStatus status,
        DateTimeOffset createdAt,
        byte[] cV)
    {
        CandidateId = candidateId;
        CandidateFirstName = candidateFirstName;
        CandidateLastName = candidateLastName;
        JobOffer = jobOffer;
        JobOfferId = jobOfferId;
        MessageToEmployer = messageToEmployer;
        Disposition = disposition;
        FinancialExpectations = financialExpectations;
        PreferredContract = preferredContract;
        Status = status;
        CreatedAt = createdAt;
        CV = cV;
    }

    private JobApplication()
    {
        // EF Core needs it
    }

    public Guid CandidateId { get; private set; }
    public string CandidateFirstName { get; private set; }
    public string CandidateLastName { get; private set; }
    public JobOffer JobOffer { get; private set; }
    public Guid JobOfferId { get; private set; }
    public string? MessageToEmployer { get; private set; }
    public Disposition Disposition { get; private set; }
    public FinancialExpectations? FinancialExpectations { get; private set; }
    public ContractType? PreferredContract { get; private set; }
    public JobApplicationStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public byte[] CV { get; private set; }
}
