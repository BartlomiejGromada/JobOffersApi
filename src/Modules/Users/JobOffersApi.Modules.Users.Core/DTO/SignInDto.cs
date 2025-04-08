using System;

namespace JobOffersApi.Modules.Users.Core.DTO;

internal class SignInDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}
