using JobOffersApi.Abstractions.Api;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Companies.Application.Commands;
using JobOffersApi.Modules.Companies.Core.DTO;
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

    private readonly IContext _context;

    public CompaniesController(IDispatcher dispatcher, IContext context) : base(dispatcher)
    {
        _context = context;
    }

    [HttpPost("{companyId:guid}/employers")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
    [SwaggerOperation("Add employer to company")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddAsync(
      [FromRoute] Guid companyId,
      [FromBody] AddEmployerToCompanyDto dto,
      CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new AddEmployerToCompanyCommand(
                companyId,
                dto.UserEmail,
                dto.Position,
                _context.Identity.Id,
                _context.Identity.Role),
            cancellationToken);

        return Created("", null);
    }

    [HttpDelete("{companyId:guid}/employers/{employerId:guid}")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Employer}")]
    [SwaggerOperation("Remove employer from company")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync(
      [FromRoute] Guid companyId,
      [FromRoute] Guid employerId,
      CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new RemoveEmployerFromCompanyCommand(
                companyId,
                employerId,
                _context.Identity.Id,
                _context.Identity.Role),
            cancellationToken);

        return NoContent();
    }
}
