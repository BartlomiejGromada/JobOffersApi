using JobOffersApi.Abstractions.Commands;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobOffersApi.Modules.Users.Core.Commands;

internal record SignInCommand(
    [Required] string Email, 
    [Required] string Password) : ICommand
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
