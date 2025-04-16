using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Api;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationsQuery;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationQuery;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationCVQuery;

namespace JobOffersApi.Modules.Users.Api.Controllers;

[Route("api/job-offers")]
[Authorize(Policy)]
internal class JobApplicationsController : BaseController
{
    private const string Policy = "jobApplications";
    private readonly IContext _context;

    public JobApplicationsController(IDispatcher dispatcher, IContext context) : base(dispatcher)
    {
        _context = context;
    }

    [HttpGet("{id:guid}/job-applications")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
    [SwaggerOperation("Get job applications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<JobApplicationDto>>> GetPagedAsync(
        [FromRoute] Guid id,
        [FromQuery] BrowseApplicationsQuery query,
        CancellationToken cancellationToken = default)
            => Ok(await dispatcher.QueryAsync(
                    new JobApplicationsQuery(id, _context.Identity.Id, _context.Identity.Role, query), cancellationToken));


    [HttpGet("{id:guid}/job-applications/{appId:guid}")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Employer},{Roles.Candidate}")]
    [SwaggerOperation("Get job applications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobApplicationDto>> GetAsync(
        [FromRoute] Guid id,
        [FromRoute] Guid appId,
        CancellationToken cancellationToken = default)

        => OkOrNotFound(await dispatcher.QueryAsync(
            new JobApplicationQuery(id, appId, _context.Identity.Id, _context.Identity.Role), cancellationToken));


    [HttpGet("{id:guid}/job-applications/{appId:guid}/cv")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Employer},{Roles.Candidate}")]
    [SwaggerOperation("Get CV from job application")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCVAsync(
        [FromRoute] Guid id,
        [FromRoute] Guid appId,
        CancellationToken cancellationToken = default)
    {
        var cvBytes = await dispatcher.QueryAsync(
            new JobApplicationCVQuery(id, appId, _context.Identity.Id, _context.Identity.Role), cancellationToken);

        if(cvBytes is null)
        {
            return NotFound();
        }

        return File(cvBytes, "application/pdf", "cv.pdf");
    }
}