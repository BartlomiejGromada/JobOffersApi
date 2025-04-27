using JobOffersApi.Abstractions.Contexts;

namespace JobOffersApi.Shared.Tests;

public sealed class TestIdentityContext : IIdentityContext
{
    private readonly Guid _id;
    private readonly string _role;
    private readonly Dictionary<string, IEnumerable<string>> _claims;
    public TestIdentityContext(
        Guid id,
        string role,
        Dictionary<string, IEnumerable<string>> claims = null)
    {
        _id = id;
        _role = role;
        _claims = claims ?? [];
    }

    public bool IsAuthenticated => true;

    public Guid Id => _id;

    public string Role => _role;

    public Dictionary<string, IEnumerable<string>> Claims => _claims;
}
