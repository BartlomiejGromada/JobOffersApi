using System;

namespace JobOffersApi.Abstractions.Exceptions;

public abstract class ModularException : Exception
{
    protected ModularException(string message) : base(message)
    {
    }
}