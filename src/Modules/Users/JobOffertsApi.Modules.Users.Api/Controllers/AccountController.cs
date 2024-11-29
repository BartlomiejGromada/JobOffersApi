using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Users.Core.Commands;
using JobOffersApi.Modules.Users.Core.Queries;
using JobOffersApi.Modules.Users.Core.DTO;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Modules.Users.Core.Storages;
using JobOffersApi.Abstractions.Api;
using JobOffersApi.Modules.Users.Integration.Queries;
using JobOffersApi.Modules.Users.Integration.DTO;

namespace JobOffersApi.Modules.Users.Api.Controllers;

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
    public async Task<ActionResult<UserDto>> GetAsync()
        => OkOrNotFound(await dispatcher.QueryAsync(new GetUserQuery { UserId = _context.Identity.Id }));


    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync(SignUpCommand command)
    {
        await dispatcher.SendAsync(command);

        return NoContent();
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDetailsDto>> SignInAsync(SignInCommand command)
    {
        await dispatcher.SendAsync(command);

        var jwt = _userRequestStorage.GetToken(command.Id);
        var user = await dispatcher.QueryAsync(new GetUserDetailsQuery { UserId = jwt.UserId });
        AddCookie(AccessTokenCookie, jwt.AccessToken);

        return Ok(user);
    }

    [Authorize]
    [HttpDelete("sign-out")]
    [SwaggerOperation("Sign out")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> SignOutAsync()
    {
        await dispatcher.SendAsync(new SignOutCommand(_context.Identity.Id));
        DeleteCookie(AccessTokenCookie);

        return NoContent();
    }

    private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, _cookieOptions);

    private void DeleteCookie(string key) => Response.Cookies.Delete(key, _cookieOptions);
}