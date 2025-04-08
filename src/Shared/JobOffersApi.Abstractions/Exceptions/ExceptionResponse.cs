using System.Net;

namespace JobOffersApi.Abstractions.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);