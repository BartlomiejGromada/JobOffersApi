using System;
using JobOffersApi.Abstractions.Auth;

namespace JobOffersApi.Modules.Users.Core.Storages;

internal interface IUserRequestStorage
{
    void SetToken(Guid commandId, JsonWebToken jwt);
    JsonWebToken GetToken(Guid commandId);
}