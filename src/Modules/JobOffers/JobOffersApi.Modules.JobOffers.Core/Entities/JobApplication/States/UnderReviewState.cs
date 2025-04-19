namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication.States;

internal class UnderReviewState : JobApplicationState
{
    public override void Apply(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Application already submitted.");
    }

    public override void Review(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Application is already under review.");
    }

    public override void ScheduleInterview(JobApplication jobApplication)
    {
        jobApplication.SetState(new InterviewScheduledState());
    }

    public override void ConductInterview(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Interview must be scheduled first.");
    }

    public override void ReceiveOffer(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Interview must be conducted before receiving an offer.");
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
