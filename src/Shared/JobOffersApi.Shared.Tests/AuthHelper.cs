using JobOffersApi.Infrastructure.Auth;
using JobOffersApi.Infrastructure.Time;
using System;
using System.Collections.Generic;


namespace JobOffersApi.Shared.Tests;

public static class AuthHelper
{
    private static readonly AuthManager AuthManager;

    static AuthHelper()
    {
        var options = OptionsHelper.GetOptions<AuthOptions>("auth");
        AuthManager = new AuthManager(options, new UtcClock());
    }

    public static string GenerateJwt(
        Guid userId,
        string role,
        IDictionary<string, IEnumerable<string>> claims = null)
        => AuthManager.CreateToken(
            userId,
            role,
            audience: null,
            claims).AccessToken;
}