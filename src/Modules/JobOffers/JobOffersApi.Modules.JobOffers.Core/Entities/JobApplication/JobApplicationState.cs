namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplication;

internal abstract class JobApplicationState
{
    public abstract void Apply(JobApplication jobApplication);
    public abstract void Review(JobApplication jobApplication);
    public abstract void ScheduleInterview(JobApplication jobApplication);
    public abstract void ConductInterview(JobApplication jobApplication);
    public abstract void ReceiveOffer(JobApplication jobApplication);
    public abstract void AcceptOffer(JobApplication jobApplication);
    public abstract void Reject(JobApplication jobApplication);
    public abstract void Withdraw(JobApplication jobApplication);
}