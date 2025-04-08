namespace JobOffersApi.Abstractions.Exceptions;

internal class InvalidMoneyException : ModularException
{
    public InvalidMoneyException() : base("Money can't be less than 0.")
    {
    }
}
