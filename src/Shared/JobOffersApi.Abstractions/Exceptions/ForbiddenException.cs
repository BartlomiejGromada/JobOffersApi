using System;

namespace JobOffersApi.Abstractions.Exceptions;

public abstract class ForbiddenException : Exception
{
    protected ForbiddenException(string message) : base(message)
    {
    }
}
