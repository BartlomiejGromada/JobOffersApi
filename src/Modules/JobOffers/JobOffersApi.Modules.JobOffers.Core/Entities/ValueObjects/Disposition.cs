using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;

namespace JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;

internal class Disposition : ValueObject
{
    public Disposition(Availability availability, DateOnly? date = null)
    {
        Availability = availability;

        if (availability != Availability.PickedDate && date is not null)
        {
            throw new InvalidAvailabilityException();
        }

        if (date is not null && date.Value < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new InvalidAvailabilityDateException(date!.Value);
        }
        Date = date;
    }

    private Disposition() { }

    public Availability Availability { get; set; }
    public DateOnly? Date { get; private set; }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Availability;
        yield return Date;
    }
}
