using Microsoft.Extensions.Logging;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL;

internal sealed class UsersInitializer : IInitializer
{
    private readonly UsersDbContext _dbContext;
    private readonly ILogger<UsersInitializer> _logger;

    public UsersInitializer(UsersDbContext dbContext, ILogger<UsersInitializer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InitAsync()
    {
        if (await _dbContext.Roles.AnyAsync())
        {
            return;
        }

        await AddRolesAsync();
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRolesAsync()
    {
        await _dbContext.Roles.AddAsync(new Role(Role.Admin, new List<string>() { "users" }));
        await _dbContext.Roles.AddAsync(new Role(Role.Employer, new List<string>() { }));
        await _dbContext.Roles.AddAsync(new Role(Role.Candidate, new List<string>() { }));

        _logger.LogInformation("Initialized roles.");
    }
}