using System;

namespace Modular.Modules.Users.Core.DTO;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}