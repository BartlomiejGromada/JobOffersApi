using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Queries;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Modules.Users.Core.Queries;
using JobOffersApi.Abstractions.Api;
using JobOffersApi.Modules.Users.Integration.DTO;
using JobOffersApi.Abstractions.Core;

namespace JobOffersApi.Modules.Users.Api.Controllers;

[Route("users")]
[Authorize(Policy)]
internal class UsersController : BaseController
{
    private const string Policy = "users";

    public UsersController(IDispatcher dispatcher) : base(dispatcher)
    {
    }
    

    [HttpGet("{userId:guid}")]
    [Authorize(Roles = $"{Roles.Admin}")]
    [SwaggerOperation("Get user details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserDetailsDto>> GetAsync(Guid userId)
        => OkOrNotFound(await dispatcher.QueryAsync(new UserDetailsQuery { UserId = userId }));

    [HttpGet]
    [Authorize(Roles = $"{Roles.Admin}")]
    [SwaggerOperation("Browse users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Paged<UserDto>>> BrowseAsync([FromQuery] BrowseUsers query)
        => Ok(await dispatcher.QueryAsync(query));
}