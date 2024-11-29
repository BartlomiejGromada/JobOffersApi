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
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Contexts;
using FluentValidation;


namespace JobOffersApi.Modules.Users.Api.Controllers;

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

    [HttpPost]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
    [SwaggerOperation("Add job offer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobOfferDetailsDto>> AddAsync(
        [FromBody] AddJobOfferDto dto,
        [FromServices] IValidator<AddJobOfferDto> validator,
        CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var userId = _context.Identity.Id;

        await dispatcher.SendAsync(
            new AddJobOfferCommand { Dto = dto, EmployerId = userId },
            cancellationToken);

        return Created("", null);
    }


    [HttpPost("{id:guid}/apply")]
    [Authorize(Roles = $"{Roles.Candidate}")]
    [SwaggerOperation("Apply for job offer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobOfferDetailsDto>> ApplyAsync(
      [FromRoute] Guid id,
      [FromForm] ApplyForJobDto dto,
      CancellationToken cancellationToken = default)
    {
        var userId = _context.Identity.Id;

        await dispatcher.SendAsync(
            new ApplyForJobOfferCommand { JobOfferId = id, Dto = dto, CandidateId = userId },
            cancellationToken);

        return Created("", null);
    }

    // TODO: Dodanie modułu z firmami
    // TODO: Dodanie endpointa do usera (admin może wyłączyć konto dla usera)
}