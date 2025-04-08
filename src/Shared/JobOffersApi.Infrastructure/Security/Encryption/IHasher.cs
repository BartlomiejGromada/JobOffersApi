namespace JobOffersApi.Infrastructure.Security.Encryption;

public interface IHasher
{
    string Hash(string data);
}