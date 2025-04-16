using System.Threading;
using System.Threading.Tasks;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Core.Storages;

namespace JobOffersApi.Modules.Users.Application.Queries.UserDetailsQuery;

internal sealed class UserDetailsQueryHandler : IQueryHandler<UserDetailsQuery, UserDetailsDto?>
{
    private readonly IUsersStorage _storage;

    public UserDetailsQueryHandler(IUsersStorage storage)
    {
        _storage = storage;
    }

    public Task<UserDetailsDto?> HandleAsync(UserDetailsQuery query, CancellationToken cancellationToken = default)
        => _storage.GetDetailsAsync(query.UserId, cancellationToken);
}