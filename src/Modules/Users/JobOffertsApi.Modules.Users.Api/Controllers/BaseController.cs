using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Infrastructure.Api;
using JobOffersApi.Abstractions.Dispatchers;

namespace JobOffersApi.Modules.Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesDefaultContentType]
internal abstract class BaseController : ControllerBase
{
    public readonly IDispatcher dispatcher;
    protected BaseController(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
    }

    protected ActionResult<T> OkOrNotFound<T>(T? model)
    {
        if (model is not null)
        {
            return Ok(model);
        }

        return NotFound();
    }
}