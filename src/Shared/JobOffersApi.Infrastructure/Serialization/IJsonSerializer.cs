using System;

namespace JobOffersApi.Infrastructure.Serialization;

public interface IJsonSerializer
{
    string Serialize<T>(T value);
    T Deserialize<T>(string value);
    object Deserialize(string value, Type type);
}