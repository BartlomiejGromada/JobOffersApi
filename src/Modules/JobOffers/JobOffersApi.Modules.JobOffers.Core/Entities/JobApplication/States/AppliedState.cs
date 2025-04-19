namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication.States;

internal class AppliedState : JobApplicationState
{
    public override void Apply(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Application already submitted.");
    }

    public override void Review(JobApplication jobApplication)
    {
        jobApplication.SetState(new UnderReviewState());
    }

    public override void ScheduleInterview(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Application must be reviewed before scheduling interview.");
    }

    public override void ConductInterview(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Interview must be scheduled first.");
    }

    public override void ReceiveOffer(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Too early to receive an offer.");
    }

    public override void AcceptOffer(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Offer must be received first.");
    }
  
    public override void Reject(JobApplication jobApplication)
    {
        jobApplication.SetState(new RejectedState());
    }

    public override void Withdraw(JobApplication jobApplication)
    {
        jobApplication.SetState(new WithdrawnState());
    }
}

