using System;

namespace JobOffersApi.Modules.Users.Core.DTO;

internal class SignUpDto
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string RepeatPassword { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Role { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
}
