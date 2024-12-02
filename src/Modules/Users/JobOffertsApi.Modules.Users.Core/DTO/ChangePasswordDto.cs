using System.ComponentModel.DataAnnotations;

namespace JobOffersApi.Modules.Users.Core.DTO;

internal class ChangePasswordDto
{
    public string Email { get; init; }
    public string CurrentPassword { get; init; }
    public string NewPassword { get; init; }
}
