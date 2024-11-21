using JobOffersApi.Abstractions.Core;

namespace JobOffersApi.Modules.JobOffers.Core.Entities.JobApplications;

internal class FinancialExpectations : ValueObject
{
    public FinancialExpectations(decimal? netto, decimal? brutto, CurrencyCode currencyCode)
    {
        Netto = netto is not null ? new Money(netto.Value, currencyCode) : null;
        Brutto = brutto is not null ? new Money(brutto.Value, currencyCode) : null;
    }

    private FinancialExpectations() { }

    public Money? Netto { get; private set; }
    public Money? Brutto { get; private set; }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Netto;
        yield return Brutto;
    }
}
