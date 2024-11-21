using System.Collections.Generic;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Modules.Users.Core.Entities;

namespace JobOffersApi.Modules.Users.Core.Queries.Handlers;

internal static class Extensions
{
    private static readonly Dictionary<UserState, string> States = new()
    {
        [UserState.Active] = UserState.Active.ToString().ToLowerInvariant(),
        [UserState.Locked] = UserState.Locked.ToString().ToLowerInvariant()
    };
    
    public static UserDto AsDto(this User user)
        => user.Map<UserDto>();

    public static UserDetailsDto AsDetailsDto(this User user)
    {
        var dto = user.Map<UserDetailsDto>();
        dto.Id = user.Id;
        dto.Permissions = user.Role.Permissions;

        return dto;
    }

    private static T Map<T>(this User user) where T : UserDto, new()
        => new()
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            RoleName = user.Role.Name,
            CreatedAt = user.CreatedAt
        };
}