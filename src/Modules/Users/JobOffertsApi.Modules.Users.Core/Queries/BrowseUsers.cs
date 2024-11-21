using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Abstractions.Queries;

namespace JobOffersApi.Modules.Users.Core.Queries;

internal class BrowseUsers : PagedQuery<UserDto>
{
    public string? Email { get; set; }
    public string? Role { get; set; }
}