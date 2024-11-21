using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Api;
using Swashbuckle.AspNetCore.Annotations;
using Polly;


namespace JobOffersApi.Modules.Users.Api.Controllers;

internal class JobOffersController : BaseController
{
    private const string Policy = "jobOffers";

    public JobOffersController(IDispatcher dispatcher) : base(dispatcher)
    {
    }
    
    [HttpGet]
    [SwaggerOperation("Get job offers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetPagedAsync()
    {
        return Ok();
    }
}