using System;

namespace JobOffersApi.Abstractions.Exceptions;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message) : base(message)
    {
    }
}
