namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

internal enum JobApplicationStatus
{
    Applied = 0,            // Złożono aplikację
    InReview = 1,           // Aplikacja jest analizowana przez rekruterów
    InterviewScheduled = 2, // Zaplanowano rozmowę kwalifikacyjną
    Interviewed = 3,        // Rozmowa się odbyła
    OfferReceived = 4,      // Kandydat otrzymał ofertę
    OfferAccepted = 5,      // Kandydate zaakceptował ofertę
    Rejected = 6,           // Kandydat został odrzucony
    Withdrawn = 7           // Kandydat sam wycofał aplikację
}