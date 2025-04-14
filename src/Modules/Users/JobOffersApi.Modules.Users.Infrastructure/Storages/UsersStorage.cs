using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Infrastructure.MsSqlServer;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Modules.Users.Infrastructure.DAL;
using JobOffersApi.Modules.Users.Integration.DTO;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.Users.Infrastructure.Storages;

internal sealed class UsersStorage : IUsersStorage
{
    private readonly IQueryable<User> _users;

    public UsersStorage(UsersDbContext dbContext)
    {
        _users = dbContext.Set<User>().AsNoTracking();
    }

    public async Task<UserDetailsDto?> GetDetailsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _users
            .SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);

        return user?.AsDetailsDto();
    }

    public async Task<Paged<UserDto>> GetPagedAsync(
        string? email,
        string? role,
        int page,
        int results,
        CancellationToken cancellationToken = default)
    {
        var users = _users;

        if (!string.IsNullOrWhiteSpace(email))
        {
            users = users.Where(x => x.Email == email);
        }

        if (!string.IsNullOrWhiteSpace(role))
        {
            users = users.Where(x => x.RoleId == role);
        }

        return await users
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => x.AsDto())
            .PaginateAsync(page, results, cancellationToken);
    }

    public async Task<UserDto?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _users
            .SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);

        return user?.AsDto();
    }

    public async Task<UserDto?> GetAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _users
            .SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

        return user?.AsDto();
    }
}
