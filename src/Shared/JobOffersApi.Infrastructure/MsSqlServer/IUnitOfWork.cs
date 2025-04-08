using System;
using System.Threading.Tasks;

namespace JobOffersApi.Infrastructure.MsSqlServer;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}