using JobOffersApi.Abstractions.Api;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.Dispatchers;
using JobOffersApi.Modules.Companies.Core.Commands;
using JobOffersApi.Modules.Companies.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobOffersApi.Modules.Companies.Api.Controllers;


[Route("companies")]
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
      [FromRoute] Guid id,
      [FromBody] AddEmployerToCompanyDto dto,
      CancellationToken cancellationToken = default)
    {
        await dispatcher.SendAsync(
            new AddEmployerToCompanyCommand(id, dto.UserEmail, dto.Position, _context.Identity.Id, _context.Identity.Role),
            cancellationToken);

        return Created("", null);
    }
}
