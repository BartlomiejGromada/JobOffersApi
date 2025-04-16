using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Application.Queries.BrowseUsers;

internal class BrowseUsers : PagedQuery<UserDto>
{
    public string? Email { get; set; }
    public string? Role { get; set; }
}