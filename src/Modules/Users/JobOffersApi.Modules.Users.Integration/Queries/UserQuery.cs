using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Integration.Queries;

public class UserQuery : IQuery<UserDto?>
{
    public Guid UserId { get; set; }
}

public class UserQueryByEmail : IQuery<UserDto?>
{
    public string Email { get; set; }
}