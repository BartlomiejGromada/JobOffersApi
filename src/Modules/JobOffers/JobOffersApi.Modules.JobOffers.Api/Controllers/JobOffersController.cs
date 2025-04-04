using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Api;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Modules.JobOffers.Core.Queries;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Modules.JobOffers.Core.Commands;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

namespace JobOffersApi.Modules.Users.Api.Controllers;

[Route("job-offers")]
[Authorize(Policy)]
internal class JobOffersController : BaseController
{
    private const string Policy = "jobOffers";
    private readonly IContext _context;

    public JobOffersController(IDispatcher dispatcher, IContext context) : base(dispatcher)
    {
        _context = context;
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
    [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
    [SwaggerOperation("Add job offer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddAsync(
        [FromBody] AddJobOfferDto dto,
        CancellationToken cancellationToken = default)
    {
        var userId = _context.Identity.Id;

        await dispatcher.SendAsync(
            new AddJobOfferCommand { Dto = dto, EmployerId = userId },
            cancellationToken);

        return Created("", null);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
    [SwaggerOperation("Remove job offer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new RemoveJobOfferCommand(id, _context.Identity.Id, _context.Identity.Role),
            cancellationToken);

        return NoContent();
    }


    [HttpPost("{id:guid}/apply")]
    [Authorize(Roles = $"{Roles.Candidate}")]
    [SwaggerOperation("Apply for job offer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ApplyAsync(
      [FromRoute] Guid id,
      [FromForm] AddJobApplicationDto dto,
      CancellationToken cancellationToken = default)
    {
        var userId = _context.Identity.Id;

        await dispatcher.SendAsync(
            new ApplyForJobOfferCommand { JobOfferId = id, Dto = dto, CandidateId = userId },
            cancellationToken);

        return Created("", null);
    }

    [HttpDelete("{id:guid}/job-application/{jobApplicationId:guid}")]
    [Authorize(Roles = $"{Roles.Candidate}")]
    [SwaggerOperation("Unapply from job offer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UnapplyAsync(
      [FromRoute] Guid id,
      [FromRoute] Guid jobApplicationId,
      CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new UnapplyFromJobOfferCommand(id, jobApplicationId, _context.Identity.Id),
            cancellationToken);

        return NoContent();
    }
}