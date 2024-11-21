using JobOffersApi.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace JobOffersApi.Modules.Users.Core.Commands;

internal record ChangePasswordCommand(
    [Required] string Email,
    [Required] string CurrentPassword,
    [Required] string NewPassword) : ICommand;