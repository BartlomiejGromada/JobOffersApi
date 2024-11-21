using System;
using JobOffersApi.Abstractions.Time;

namespace JobOffersApi.Infrastructure.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}