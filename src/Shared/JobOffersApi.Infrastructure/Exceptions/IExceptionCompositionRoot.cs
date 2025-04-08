using System;
using JobOffersApi.Abstractions.Exceptions;

namespace JobOffersApi.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}