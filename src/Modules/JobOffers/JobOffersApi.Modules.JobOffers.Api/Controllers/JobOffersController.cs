using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Api;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Modules.JobOffers.Core.Queries;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.DTO;
using JobOffersApi.Modules.JobOffers.Core.Commands;


namespace JobOffersApi.Modules.Users.Api.Controllers;

[Authorize(Policy)]
internal class JobOffersController : BaseController
{
    private const string Policy = "jobOffers";

    public JobOffersController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get job offers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<JobOfferDto>>> GetPagedAsync(
        [FromQuery] BrowseJobOffersQuery query,
        CancellationToken cancellationToken = default)
            => Ok(await dispatcher.QueryAsync(query, cancellationToken));


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [SwaggerOperation("Get job offer details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobOfferDetailsDto>> GetAsync(
        [FromRoute] Guid id,
         CancellationToken cancellationToken = default)
            => OkOrNotFound(await dispatcher.QueryAsync(new GetJobOfferQuery(id), cancellationToken));

    [HttpPost()]
    [SwaggerOperation("Add job offer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobOfferDetailsDto>> AddAsync(
        [FromBody] AddJobOfferCommand command,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(command, cancellationToken);

        return Created("", null);
    }


    // TODO: Dodawanie aplikacji na ofertę
    // TODO: Dodanie modułu z firmami
    // TODO: Dodanie endpointa do usera (admin może wyłączyć konto dla usera)
}