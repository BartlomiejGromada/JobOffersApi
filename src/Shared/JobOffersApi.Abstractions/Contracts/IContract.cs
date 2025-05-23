using System;
using System.Collections.Generic;

namespace JobOffersApi.Abstractions.Contracts;

public interface IContract
{
    Type Type { get; }
    public IEnumerable<string> Required { get; }
}