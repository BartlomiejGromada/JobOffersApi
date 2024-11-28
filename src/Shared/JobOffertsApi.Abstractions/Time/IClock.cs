using System;

namespace JobOffersApi.Abstractions.Time;

public interface IClock
{
    DateTimeOffset CurrentDate();
}