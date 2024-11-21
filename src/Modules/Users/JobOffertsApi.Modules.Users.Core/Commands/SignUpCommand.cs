using System;
using System.ComponentModel.DataAnnotations;
using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Core.Commands;

internal record SignUpCommand(
    [Required][EmailAddress] string Email,
    [Required] string Password,
    [Required] string RepeatPassword,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string Role,
    [Required] DateTimeOffset DateOfBirth) : ICommand;