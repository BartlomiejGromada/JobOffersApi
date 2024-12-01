using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Modules.Users.Core.Commands;
using JobOffersApi.Abstractions.Api;

namespace JobOffersApi.Modules.Users.Api.Controllers;

[Route("passwords")]
internal class PasswordController : BaseController
{

    public PasswordController(IDispatcher dispatcher) : base(dispatcher)
    {
    }
    
    [Authorize]
    [HttpPut]
    [SwaggerOperation("Change password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ChangeAsync(ChangePasswordCommand command)
    {
        await dispatcher.SendAsync(command);
        return NoContent();
    }
}