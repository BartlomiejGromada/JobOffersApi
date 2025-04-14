using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.Users.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace JobOffersApi.Modules.Users.Application.Commands;

internal record UpdateUserStateCommand(
    [Required] Guid UserId, 
    [Required] UserState State) : ICommand;
