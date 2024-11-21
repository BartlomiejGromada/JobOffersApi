using System.Threading;
using System.Threading.Tasks;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Core.Storages;

namespace JobOffersApi.Modules.Users.Core.Queries.Handlers;

internal sealed class GetUserDetailsQueryHandler : IQueryHandler<GetUserDetailsQuery, UserDetailsDto?>
{
    private readonly IUsersStorage _storage;

    public GetUserDetailsQueryHandler(IUsersStorage storage)
    {
        _storage = storage;
    }

    public Task<UserDetailsDto?> HandleAsync(GetUserDetailsQuery query, CancellationToken cancellationToken = default)
        => _storage.GetDetailsAsync(query.UserId, cancellationToken);
}