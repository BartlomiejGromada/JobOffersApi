using JobOffersApi.Modules.Users.Integration.DTO;
using System;
using System.Collections.Generic;

namespace JobOffersApi.Modules.Users.Core.DTO;

internal class UserDetailsDto : UserDto
{
    public Guid Id { get; set; }
    public IEnumerable<string> Permissions { get; set; }
}