using System;
using System.ComponentModel.DataAnnotations;
using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Application.Commands;

internal record SignOutCommand([Required] Guid UserId) : ICommand;