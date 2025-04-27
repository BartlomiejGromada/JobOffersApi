using JobOffersApi.Abstractions.Time;

namespace JobOffersApi.Shared.Tests;

public class TestClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;

    public DateTimeOffset CurrentDateOffset() => DateTimeOffset.UtcNow;
}
