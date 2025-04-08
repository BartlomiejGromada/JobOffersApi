using System.Collections.Generic;

namespace JobOffersApi.Modules.Users.Core.Entities;

internal class Role
{
    private readonly List<User> _users = new();
    private readonly List<string> _permissions;

    private Role()
    {
        // EF CORE needs it
    }

    public Role(string name, List<string> permissions)
    {
        Name = name;
        _permissions = permissions;
    }

    public string Name { get; private set; }
    public IReadOnlyCollection<string> Permissions => _permissions;
    public IReadOnlyCollection<User> Users => _users;
}