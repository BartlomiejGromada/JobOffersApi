namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication.States;

internal class RejectedState : JobApplicationState
{
    public override void Apply(JobApplication jobApplication) => ThrowFinal();
    public override void Review(JobApplication jobApplication) => ThrowFinal();
    public override void ScheduleInterview(JobApplication jobApplication) => ThrowFinal();
    public override void ConductInterview(JobApplication jobApplication) => ThrowFinal();
    public override void ReceiveOffer(JobApplication jobApplication) => ThrowFinal();
    public override void AcceptOffer(JobApplication jobApplication) => ThrowFinal();
    public override void Reject(JobApplication jobApplication) => ThrowFinal();
    public override void Withdraw(JobApplication jobApplication) => ThrowFinal();

    private void ThrowFinal() => throw new InvalidOperationException("Application rejected. No further transitions.");
}