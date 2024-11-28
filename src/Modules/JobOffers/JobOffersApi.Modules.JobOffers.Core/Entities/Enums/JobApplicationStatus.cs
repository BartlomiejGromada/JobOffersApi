namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

internal enum JobApplicationStatus
{
    Submitted, // aplikacja przesłana
    UnderReview, // w trakcie weryfikacji
    Rejected, // odrzucona
    Withdrawn // kandydat się wycofał
}
