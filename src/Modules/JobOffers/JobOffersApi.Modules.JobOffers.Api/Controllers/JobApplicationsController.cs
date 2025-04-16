using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Abstractions.Api;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Abstractions.Queries;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationsQuery;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationQuery;
using JobOffersApi.Modules.JobOffers.Application.Queries.JobApplicationCVQuery;

namespace JobOffersApi.Modules.JobOffers.Api.Controllers;

[Route("api/job-offers")]
[Authorize(Policy)]
internal class JobApplicationsController : BaseController
{
    private const string Policy = "jobApplications";

    public JobApplicationsController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet("{id:guid}/job-applications")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.OwnerCompany},{Roles.Employer}")]
    [SwaggerOperation("Get job applications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<JobApplicationDto>>> GetPagedAsync(
        [FromRoute] Guid id,
        [FromQuery] BrowseApplicationsQuery query,
        CancellationToken cancellationToken = default)
            => Ok(await dispatcher.QueryAsync(
                    new JobApplicationsQuery(id, query), cancellationToken));


    [HttpGet("{id:guid}/job-applications/{applicationId:guid}")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.OwnerCompany},{Roles.Employer},{Roles.Candidate}")]
    [SwaggerOperation("Get job applications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobApplicationDto>> GetAsync(
        [FromRoute] Guid id,
        [FromRoute] Guid applicationId,
        CancellationToken cancellationToken = default)

        => OkOrNotFound(await dispatcher.QueryAsync(
            new JobApplicationQuery(id, applicationId), cancellationToken));


    [HttpGet("{id:guid}/job-applications/{applicationId:guid}/cv")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.OwnerCompany},{Roles.Employer},{Roles.Candidate}")]
    [SwaggerOperation("Get CV from job application")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCVAsync(
        [FromRoute] Guid id,
        [FromRoute] Guid applicationId,
        CancellationToken cancellationToken = default)
    {
        var cvBytes = await dispatcher.QueryAsync(
            new JobApplicationCVQuery(id, applicationId), cancellationToken);

        if (cvBytes is null)
        {
            return NotFound();
        }

        return File(cvBytes, "application/pdf", "cv.pdf");
    }
}