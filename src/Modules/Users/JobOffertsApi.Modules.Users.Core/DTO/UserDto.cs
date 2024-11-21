using JobOffersApi.Modules.Users.Core.Entities;
using System;

namespace JobOffersApi.Modules.Users.Core.DTO;

internal class UserDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string RoleName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}