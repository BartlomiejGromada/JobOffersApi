using System.Threading.Tasks;

namespace JobOffersApi.Infrastructure;

public interface IInitializer
{
    Task InitAsync();
}