using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Integration.Services;

public interface IUsersService
{
    Task<UserDto> GetAsync(Guid userId, CancellationToken cancellationToken = default);
}
