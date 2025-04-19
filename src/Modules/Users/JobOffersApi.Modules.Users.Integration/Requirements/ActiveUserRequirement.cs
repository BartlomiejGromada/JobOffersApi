using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Users.Integration.Exceptions;
using JobOffersApi.Modules.Users.Integration.Queries;
using Microsoft.AspNetCore.Authorization;

namespace JobOffersApi.Modules.Users.Integration.Requirements;

public class ActiveUserRequirement : IAuthorizationRequirement { }

internal sealed class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
{
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public ActiveUserHandler(IDispatcher dispatcher, IContext context)
    {
        _dispatcher = dispatcher;
        _context = context;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, ActiveUserRequirement requirement)
    {
        var identity = _context.Identity;

        var user = await _dispatcher.QueryAsync(new UserQuery { UserId = identity.Id });

        if (user is null)
        {
            throw new UserNotFoundException(identity.Id);
        }

        if (user.IsLocked)
        {
            throw new UserNotActiveException(identity.Id);
        }

        context.Succeed(requirement);
    }
}