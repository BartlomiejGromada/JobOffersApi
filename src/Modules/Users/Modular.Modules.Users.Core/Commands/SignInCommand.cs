using System;
using System.ComponentModel.DataAnnotations;
using Modular.Abstractions.Commands;

namespace Modular.Modules.Users.Core.Commands;

internal record SignInCommand([Required] string Email, [Required] string Password) : ICommand;