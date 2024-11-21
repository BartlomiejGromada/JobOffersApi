using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Modules.Users.Core.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersApi.Modules.Users.Core.Storages;

internal interface IUsersStorage
{
    public Task<UserDetailsDto?> GetDetailsAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<Paged<UserDto>> GetPagedAsync(BrowseUsers query, CancellationToken cancellationToken = default);
    public Task<UserDto?> GetAsync(Guid userId, CancellationToken cancellationToken = default);
}
