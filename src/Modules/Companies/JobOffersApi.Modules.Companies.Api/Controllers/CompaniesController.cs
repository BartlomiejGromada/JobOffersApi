using JobOffersApi.Abstractions.Api;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Companies.Application.Commands;
using JobOffersApi.Modules.Companies.Core.DTO.Companies;
using JobOffersApi.Modules.Companies.Core.DTO.Employers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobOffersApi.Modules.Companies.Api.Controllers;


[Route("api/companies")]
[Authorize(Policy)]
internal class CompaniesController : BaseController
{
    private const string Policy = "companies";

    public CompaniesController(IDispatcher dispatcher) : base(dispatcher)
    {
    }


    [HttpPost]
    [Authorize(Roles = $"{Roles.OwnerCompany}")]
    [SwaggerOperation("Add company")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddAsync(
      [FromBody] AddCompanyDto dto,
      CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new AddCompanyCommand {
                Name = dto.Name,
                Description = dto.Description,
            },
            cancellationToken);

        return Created("", null);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Roles.OwnerCompany}")]
    [Authorize(Policy = "CompanyOwnershipRequirement")]
    [SwaggerOperation("Remove company")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new RemoveCompanyCommand
            {
                Id = id,
            },
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{companyId:guid}/employers")]
    [Authorize(Roles = $"{Roles.OwnerCompany},{Roles.Employer}")]
    [Authorize(Policy = "CompanyMembershipRequirement")]
    [SwaggerOperation("Add employer to company")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddEmployerAsync(
      [FromRoute] Guid companyId,
      [FromBody] AddEmployerToCompanyDto dto,
      CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new AddEmployerToCompanyCommand(
                companyId,
                dto.UserId,
                dto.Position),
            cancellationToken);

        return Created("", null);
    }

    [HttpDelete("{companyId:guid}/employers/{employerId:guid}")]
    [Authorize(Roles = $"{Roles.OwnerCompany},{Roles.Employer}")]
    [Authorize(Policy = "CompanyMembershipRequirement")]
    [SwaggerOperation("Remove employer from company")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveEmployerAsync(
      [FromRoute] Guid companyId,
      [FromRoute] Guid employerId,
      CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new RemoveEmployerFromCompanyCommand(
                companyId,
                employerId),
            cancellationToken);

        return NoContent();
    }
}
