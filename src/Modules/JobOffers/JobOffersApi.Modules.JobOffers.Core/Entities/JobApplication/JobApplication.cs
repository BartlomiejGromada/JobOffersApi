using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication.States;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication;

internal class JobApplication : Entity<Guid>
{
    [NotMapped]
    private JobApplicationState _state;

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
        Status = JobApplicationStatus.Applied;
        CreatedAt = createdAt;
        CV = cv;
        _state = new AppliedState();
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

    public void SetState(JobApplicationState state)
    {
        _state = state;
    }

    private void SetStatus(JobApplicationStatus status)
    {
        Status = status;
        _state = status switch
        {
            JobApplicationStatus.Applied => new AppliedState(),
            JobApplicationStatus.InReview => new UnderReviewState(),
            JobApplicationStatus.InterviewScheduled => new InterviewScheduledState(),
            JobApplicationStatus.Interviewed => new InterviewedState(),
            JobApplicationStatus.OfferReceived => new OfferReceivedState(),
            JobApplicationStatus.OfferAccepted => new OfferAcceptedState(),
            JobApplicationStatus.Rejected => new RejectedState(),
            JobApplicationStatus.Withdrawn => new WithdrawnState(),
            _ => throw new ArgumentOutOfRangeException(nameof(status), $"Unknown status {status}")
        };
    }

    public void Apply()
    {
        _state.Apply(this);
        SetStatus(JobApplicationStatus.Applied);
    }

    public void Review()
    {
        _state.Review(this);
        SetStatus(JobApplicationStatus.InReview);
    }

    public void ScheduleInterview()
    {
        _state.ScheduleInterview(this);
        SetStatus(JobApplicationStatus.InterviewScheduled);
    }

    public void ConductInterview()
    {
        _state.ConductInterview(this);
        SetStatus(JobApplicationStatus.Interviewed);
    }

    public void ReceiveOffer()
    {
        _state.ReceiveOffer(this);
        SetStatus(JobApplicationStatus.OfferReceived);
    }

    public void AcceptOffer()
    {
        _state.AcceptOffer(this);
        SetStatus(JobApplicationStatus.OfferAccepted);
    }

    public void Reject()
    {
        _state.Reject(this);
        SetStatus(JobApplicationStatus.Rejected);
    }

    public void Withdraw()
    {
        _state.Withdraw(this);
        SetStatus(JobApplicationStatus.Withdrawn);
    }

    /// <summary>
    /// Needed after downloading entity from EF Core
    /// </summary>
    public void RestoreStateFromStatus()
    {
        SetStatus(Status);
    }
}
