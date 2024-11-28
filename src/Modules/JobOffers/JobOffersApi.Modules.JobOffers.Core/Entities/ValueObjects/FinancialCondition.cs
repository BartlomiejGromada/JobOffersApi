using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.Enums;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

namespace JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;

internal class FinancialCondition : ValueObject
{
    public FinancialCondition(
        decimal value,
        CurrencyCode currencyCode,
        SalaryType salaryType,
        SalaryPeriod salaryPeriod)
    {
        Value = new Money(value, currencyCode);
        SalaryType = salaryType;
        SalaryPeriod = salaryPeriod;
    }

    private FinancialCondition() { }

    public Money Value { get; private set; }
    public SalaryType SalaryType { get; private set; }
    public SalaryPeriod SalaryPeriod { get; private set; }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
        yield return SalaryType;
        yield return SalaryPeriod;
    }
}
