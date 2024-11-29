using System.Threading;
using System.Threading.Tasks;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Core.Queries.Handlers;

internal sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, Paged<UserDto>>
{
    private readonly IUsersStorage _storage;

    public BrowseUsersHandler(IUsersStorage storage)
    {
        _storage = storage;
    }

    public Task<Paged<UserDto>> HandleAsync(BrowseUsers query, CancellationToken cancellationToken = default)
        => _storage.GetPagedAsync(query, cancellationToken);
}