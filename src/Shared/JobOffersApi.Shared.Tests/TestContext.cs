using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;

namespace JobOffersApi.Shared.Tests;

public class TestContext : IContext
{
    public Guid RequestId { get; } = Guid.NewGuid();
    public Guid CorrelationId { get; }
    public string TraceId { get; }
    public string IpAddress { get; }
    public string UserAgent { get; }
    public IIdentityContext Identity { get; }

    public TestContext() : this(Guid.NewGuid(), $"{Guid.NewGuid():N}", null)
    {
    }

    public TestContext(HttpContext context) : this(Guid.Empty, context.TraceIdentifier,
        new IdentityContext(context.User), string.Empty,
        context.Request.Headers["user-agent"])
    {
    }

    public TestContext(Guid? correlationId, string traceId, IIdentityContext identity = null, string ipAddress = null,
        string userAgent = null)
    {
        CorrelationId = correlationId ?? Guid.NewGuid();
        TraceId = traceId;
        Identity = identity ?? IdentityContext.Empty;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }

    public static IContext Empty => new TestContext();
}
