using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Users.Core.DTO;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Abstractions.Api;
using JobOffersApi.Modules.Users.Integration.Queries;
using JobOffersApi.Modules.Users.Integration.DTO;
using System.Threading;
using JobOffersApi.Modules.Users.Application.Commands.SignUpCommand;
using JobOffersApi.Modules.Users.Application.Commands.SignInCommand;
using JobOffersApi.Modules.Users.Application.Commands.SignOutCommand;
using JobOffersApi.Modules.Users.Application.Queries.UserDetailsQuery;

namespace JobOffersApi.Modules.Users.Api.Controllers;

[Route("api/accounts")]
internal class AccountController : BaseController
{
    private const string AccessTokenCookie = "__access-token";
    private readonly IContext _context;
    private readonly IUserRequestStorage _userRequestStorage;
    private readonly CookieOptions _cookieOptions;

    public AccountController(
        IDispatcher dispatcher, 
        IContext context,
        IUserRequestStorage userRequestStorage,
        CookieOptions cookieOptions) : base(dispatcher)
    {
        _context = context;
        _userRequestStorage = userRequestStorage;
        _cookieOptions = cookieOptions;
    }

    [HttpGet]
    [Authorize]
    [SwaggerOperation("Get account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> GetAsync(CancellationToken cancellationToken = default)
        => OkOrNotFound(await dispatcher.QueryAsync(
            new UserQuery { UserId = _context.Identity.Id }, cancellationToken));


    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync([FromBody] SignUpDto dto,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(new SignUpCommand(
            dto.Email,
            dto.Password,
            dto.RepeatPassword,
            dto.FirstName,
            dto.LastName,
            dto.Role,
            dto.DateOfBirth), cancellationToken);

        return NoContent();
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDetailsDto>> SignInAsync(SignInDto dto,
        CancellationToken cancellationToken = default)
    {
        var command = new SignInCommand(dto.Email, dto.Password);
        await dispatcher.SendAsync(command, cancellationToken);

        var jwt = _userRequestStorage.GetToken(command.Id);
        var user = await dispatcher.QueryAsync(
            new UserDetailsQuery { UserId = jwt.UserId }, cancellationToken);
        AddCookie(AccessTokenCookie, jwt.AccessToken);

        return Ok(user);
    }

    [Authorize]
    [HttpDelete("sign-out")]
    [SwaggerOperation("Sign out")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> SignOutAsync(CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(new SignOutCommand(_context.Identity.Id),
            cancellationToken);
        DeleteCookie(AccessTokenCookie);

        return NoContent();
    }

    private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, _cookieOptions);

    private void DeleteCookie(string key) => Response.Cookies.Delete(key, _cookieOptions);
}