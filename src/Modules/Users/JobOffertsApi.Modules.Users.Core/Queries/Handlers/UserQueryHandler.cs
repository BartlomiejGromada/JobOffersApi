using System.Threading;
using System.Threading.Tasks;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Modules.Users.Integration.Queries;
using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Core.Queries.Handlers;

internal sealed class UserQueryHandler : IQueryHandler<UserQuery, UserDto?>
{
    private readonly IUsersStorage _storage;

    public UserQueryHandler(IUsersStorage storage)
    {
        _storage = storage;
    }

    public Task<UserDto?> HandleAsync(UserQuery query, CancellationToken cancellationToken = default)
        => _storage.GetAsync(query.UserId, cancellationToken);
}

internal sealed class UserQueryByEmailHandler : IQueryHandler<UserQueryByEmail, UserDto?>
{
    private readonly IUsersStorage _storage;

    public UserQueryByEmailHandler(IUsersStorage storage)
    {
        _storage = storage;
    }

    public Task<UserDto?> HandleAsync(UserQueryByEmail query, CancellationToken cancellationToken = default)
        => _storage.GetAsync(query.Email, cancellationToken);
}