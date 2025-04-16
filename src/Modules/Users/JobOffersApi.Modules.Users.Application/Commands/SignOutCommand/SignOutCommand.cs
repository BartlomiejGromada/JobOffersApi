using System.ComponentModel.DataAnnotations;
using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Users.Application.Commands.SignOutCommand;

internal record SignOutCommand([Required] Guid UserId) : ICommand;