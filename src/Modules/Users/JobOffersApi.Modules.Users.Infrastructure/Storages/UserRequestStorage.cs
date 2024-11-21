using JobOffersApi.Abstractions.Auth;
using JobOffersApi.Abstractions.Storage;

namespace JobOffersApi.Modules.Users.Core.Storages;

internal sealed class UserRequestStorage : IUserRequestStorage
{
    private readonly IRequestStorage _requestStorage;

    public UserRequestStorage(IRequestStorage requestStorage)
    {
        _requestStorage = requestStorage;
    }

    public void SetToken(Guid commandId, JsonWebToken jwt)
        => _requestStorage.Set(GetKey(commandId), jwt);

    public JsonWebToken GetToken(Guid commandId)
        => _requestStorage.Get<JsonWebToken>(GetKey(commandId));

    private static string GetKey(Guid commandId) => $"jwt:{commandId:N}";
}