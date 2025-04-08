using System;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Abstractions.Queries;

namespace JobOffersApi.Modules.Users.Core.Queries;

internal class UserDetailsQuery : IQuery<UserDetailsDto?>
{
    public Guid UserId { get; set; }
}