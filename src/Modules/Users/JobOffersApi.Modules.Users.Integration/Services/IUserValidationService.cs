using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Integration.Services;

public interface IUserValidationService
{
    Task<UserDto> ValidateAsync(Guid userId, CancellationToken cancellationToken = default);
}
