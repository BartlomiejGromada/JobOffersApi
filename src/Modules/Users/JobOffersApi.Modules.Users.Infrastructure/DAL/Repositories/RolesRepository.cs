using Microsoft.EntityFrameworkCore;
using JobOffersApi.Modules.Users.Core.Entities;
using JobOffersApi.Modules.Users.Core.Repositories;

namespace JobOffersApi.Modules.Users.Infrastructure.DAL.Repositories;

internal class RolesRepository : IRolesRepository
{
    private readonly UsersDbContext _context;
    private readonly DbSet<Role> _roles;

    public RolesRepository(UsersDbContext context)
    {
        _context = context;
        _roles = context.Roles;
    }

    public Task<Role?> GetAsync(string name) => _roles.SingleOrDefaultAsync(x => x.Name == name);

    public async Task<IReadOnlyList<Role>> GetAllAsync() => await _roles.ToListAsync();
    
    public async Task AddAsync(Role role)
    {
        await _roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }
}