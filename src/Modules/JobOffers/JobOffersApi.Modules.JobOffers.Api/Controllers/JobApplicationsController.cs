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
using JobOffersApi.Modules.JobOffers.Application.Commands.ApplyForJobOfferCommand;
using JobOffersApi.Modules.JobOffers.Application.Commands.WithdrawJobApplicationCommand;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;
using JobOffersApi.Modules.JobOffers.Application.Commands.ChangeStatusOfJobApplicationCommand;
using Asp.Versioning;

namespace JobOffersApi.Modules.JobOffers.Api.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/job-offers")]
[Authorize(Policy)]
internal class JobApplicationsController : BaseController
{
    private const string Policy = "job-applications";

    public JobApplicationsController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet("{id:guid}/job-applications")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.CompanyOwner},{Roles.Employer}")]
    [SwaggerOperation("Get job applications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<JobApplicationDto>>> GetPagedAsync(
        [FromRoute] Guid id,
        [FromQuery] BrowseApplicationsQuery query,
        CancellationToken cancellationToken = default)
            => Ok(await dispatcher.QueryAsync(
                    new JobApplicationsQuery(id, query), cancellationToken));

    [HttpGet("{id:guid}/job-applications/{applicationId:guid}")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.CompanyOwner},{Roles.Employer},{Roles.Candidate}")]
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
    [Authorize(Roles = $"{Roles.Admin},{Roles.CompanyOwner},{Roles.Employer},{Roles.Candidate}")]
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

    [HttpPost("{id:guid}/job-applications")]
    [Authorize(Roles = $"{Roles.Candidate}")]
    [SwaggerOperation("Apply for job offer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ApplyAsync(
    [FromRoute] Guid id,
    [FromForm] AddJobApplicationDto dto,
    CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new ApplyForJobOfferCommand { JobOfferId = id, Dto = dto },
            cancellationToken);

        return Created("", null);
    }

    [HttpDelete("{id:guid}/job-applications/{applicationId:guid}")]
    [Authorize(Roles = $"{Roles.Candidate}")]
    [SwaggerOperation("Withdraw job offer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> WithdrawAsync(
       [FromRoute] Guid id,
       [FromRoute] Guid applicationId,
       CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new WithdrawJobApplicationCommand
            { 
                JobOfferId = id,
                JobApplicationId = applicationId 
            },
            cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/job-applications/{applicationId:guid}")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.CompanyOwner},{Roles.Employer}")]
    [SwaggerOperation("Change status of job application")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangeStatusAsync(
        [FromRoute] Guid id,
        [FromRoute] Guid applicationId,
        [FromBody] JobApplicationStatus status,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new ChangeStatusOfJobApplicationCommand { JobOfferId = id, JobApplicationId = applicationId, Status = status },
            cancellationToken);

        return Created("", null);
    }
}