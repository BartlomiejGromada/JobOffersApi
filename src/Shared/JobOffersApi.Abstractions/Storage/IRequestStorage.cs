using System;

namespace JobOffersApi.Abstractions.Storage;

public interface IRequestStorage
{
    void Set<T>(string key, T value, TimeSpan? duration = null);
    T Get<T>(string key);
}