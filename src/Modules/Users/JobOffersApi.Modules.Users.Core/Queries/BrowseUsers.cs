using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Core.Queries;

internal class BrowseUsers : PagedQuery<UserDto>
{
    public string? Email { get; set; }
    public string? Role { get; set; }
}