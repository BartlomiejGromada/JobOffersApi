using JobOffersApi.Modules.Users.Integration.DTO;
using System;
using System.Collections.Generic;

namespace JobOffersApi.Modules.Users.Core.DTO;

internal class UserDetailsDto : UserDto
{
    public IEnumerable<string> Permissions { get; set; }
}