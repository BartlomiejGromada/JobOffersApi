using System.Threading;
using System.Threading.Tasks;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Core.Storages;

namespace JobOffersApi.Modules.Users.Core.Queries.Handlers;

internal sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto?>
{
    private readonly IUsersStorage _storage;

    public GetUserQueryHandler(IUsersStorage storage)
    {
        _storage = storage;
    }

    public Task<UserDto?> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
        => _storage.GetAsync(query.UserId, cancellationToken);
}