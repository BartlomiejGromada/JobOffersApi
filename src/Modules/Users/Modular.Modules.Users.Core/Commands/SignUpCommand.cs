using System;
using System.ComponentModel.DataAnnotations;
using Modular.Abstractions.Commands;

namespace Modular.Modules.Users.Core.Commands;

internal record SignUpCommand(
    [Required][EmailAddress] string Email,
    [Required] string Password,
    [Required] string RepeatPassword,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateOnly DateOfBirth) : ICommand;