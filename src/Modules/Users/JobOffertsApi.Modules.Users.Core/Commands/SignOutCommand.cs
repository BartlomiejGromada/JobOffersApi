using System;
using System.ComponentModel.DataAnnotations;
using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Core.Commands;

internal record SignOutCommand([Required] Guid UserId) : ICommand;