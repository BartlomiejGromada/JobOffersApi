﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobOffersApi.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;
using JobOffersApi.Abstractions.Api;
using JobOffersApi.Modules.Users.Core.DTO;
using JobOffersApi.Modules.Users.Application.Commands.ChangePasswordCommand;
using Asp.Versioning;

namespace JobOffersApi.Modules.Users.Api.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/passwords")]
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
    public async Task<ActionResult> ChangeAsync([FromBody] ChangePasswordDto dto)
    {
        await dispatcher.SendAsync(new ChangePasswordCommand(dto.Email, dto.CurrentPassword, dto.NewPassword));
        return NoContent();
    }
}