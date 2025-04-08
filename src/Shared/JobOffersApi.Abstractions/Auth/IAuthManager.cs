using System;
using System.Collections.Generic;

namespace JobOffersApi.Abstractions.Auth;

public interface IAuthManager
{
    JsonWebToken CreateToken(
        Guid userId,
        string role,
        string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null);
}