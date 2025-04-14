using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Modules.Users.Integration.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersApi.Modules.Users.Core.Storages;

internal interface IUsersStorage
{
    public Task<UserDetailsDto?> GetDetailsAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<Paged<UserDto>> GetPagedAsync(
            string? email,
            string? role,
            int page,
            int results,
            CancellationToken cancellationToken = default);
    public Task<UserDto?> GetAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<UserDto?> GetAsync(string email, CancellationToken cancellationToken = default);
}
