using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Api;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobOffersQuery;
using JobOffersApi.Modules.JobOffers.Application.Commands.RemoveJobOfferCommand;
using JobOffersApi.Modules.JobOffers.Application.Commands.AddJobOfferCommand;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobOfferQuery;
using JobOffersApi.Modules.JobOffers.Application.Commands.UpdateJobOfferCommand;
using Asp.Versioning;

namespace JobOffersApi.Modules.Users.Api.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/job-offers")]
[Authorize(Policy)]
internal class JobOffersController : BaseController
{
    private const string Policy = "job-offers";

    public JobOffersController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get job offers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<JobOfferDto>>> GetPagedAsync(
        [FromQuery] JobOffersQuery query,
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
            => OkOrNotFound(await dispatcher.QueryAsync(new JobOfferQuery(id), cancellationToken));

    [HttpPost]
    [Authorize(Roles = $"{Roles.CompanyOwner},{Roles.Admin},{Roles.Employer}")]
    [SwaggerOperation("Add job offer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddAsync(
        [FromBody] AddJobOfferDto dto,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new AddJobOfferCommand { Dto = dto },
            cancellationToken);

        return Created("", null);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = $"{Roles.CompanyOwner},{Roles.Admin},{Roles.Employer}")]
    [SwaggerOperation("Update job offer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAsync(
       [FromRoute] Guid id,
       [FromBody] UpdateJobOfferDto dto,
       CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new UpdateJobOfferCommand(id, dto),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.CompanyOwner},{Roles.Employer}")]
    [SwaggerOperation("Remove job offer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new RemoveJobOfferCommand(id),
            cancellationToken);

        return NoContent();
    }
}