using JobOffersApi.Abstractions.Exceptions;
using System.Collections.Generic;

namespace JobOffersApi.Abstractions.Core;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public CurrencyCode Currency { get; private set; }

    private Money()
    {
        // EF core needs it
    }

    public Money(decimal amount, CurrencyCode currency)
    {
        if (amount < 0)
        {
            throw new InvalidMoneyException();
        }

        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}
