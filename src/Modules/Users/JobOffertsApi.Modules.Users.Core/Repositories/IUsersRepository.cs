using System;
using System.Threading.Tasks;
using JobOffersApi.Modules.Users.Core.Entities;

namespace JobOffersApi.Modules.Users.Core.Repositories;

internal interface IUsersRepository
{
    Task<User?> GetAsync(Guid id);
    Task<User?> GetAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}