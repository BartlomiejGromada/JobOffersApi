namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication.States;

internal class InterviewScheduledState : JobApplicationState
{
    public override void Apply(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Application already submitted.");
    }

    public override void Review(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Already reviewed.");
    }

    public override void ScheduleInterview(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Interview already scheduled.");
    }

    public override void ConductInterview(JobApplication jobApplication)
    {
        jobApplication.SetState(new InterviewedState());
    }

    public override void ReceiveOffer(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Interview must be conducted first.");
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
