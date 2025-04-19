namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication.States;

internal class OfferReceivedState : JobApplicationState
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
        throw new InvalidOperationException("Interview already conducted.");
    }

    public override void ReceiveOffer(JobApplication jobApplication)
    {
        throw new InvalidOperationException("Offer already received.");
    }

    public override void Reject(JobApplication jobApplication)
    {
       jobApplication.SetState(new RejectedState());
    }

    public override void AcceptOffer(JobApplication jobApplication)
    {
        jobApplication.SetState(new OfferAcceptedState());
    }

    public override void Withdraw(JobApplication jobApplication)
    {
        jobApplication.SetState(new WithdrawnState());
    }
}