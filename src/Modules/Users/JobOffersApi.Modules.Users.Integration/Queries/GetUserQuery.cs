using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Integration.Queries;

public class GetUserQuery : IQuery<UserDto?>
{
    public Guid UserId { get; set; }
}
