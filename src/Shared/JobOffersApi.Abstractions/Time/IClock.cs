using System;

namespace JobOffersApi.Abstractions.Time;

public interface IClock
{
    DateTime CurrentDate();
    DateTimeOffset CurrentDateOffset();
}