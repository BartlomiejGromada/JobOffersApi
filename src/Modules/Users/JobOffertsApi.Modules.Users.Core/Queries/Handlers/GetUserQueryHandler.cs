using System.Threading;
using System.Threading.Tasks;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Modules.Users.Integration.Queries;
using JobOffersApi.Modules.Users.Integration.DTO;

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