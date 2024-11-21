using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.Users.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobOffersApi.Modules.Users.Core.Commands;

internal record UpdateUserStateCommand(
    [Required] Guid UserId, 
    [Required] UserState State) : ICommand;
