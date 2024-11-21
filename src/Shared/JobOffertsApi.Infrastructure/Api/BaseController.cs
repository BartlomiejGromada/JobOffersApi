using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersApi.Abstractions.Api;

[ApiController]
[Route("[controller]")]
[ProducesDefaultContentType]
public abstract class BaseController : ControllerBase
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
