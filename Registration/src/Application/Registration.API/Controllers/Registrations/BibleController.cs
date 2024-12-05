﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Registration.Handlers.Handlers.Registrations;
using Registration.Mapper.DTOs.Registration.Bible;

namespace Registration.API.Controllers.Registrations;

public class BibleController : ControllerBase
{
    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/bible")]
    public async Task<IActionResult> Get([FromServices] BibleHandler handler, [FromBody] GetBibleDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        DomainCore.ViewModelAbstraction.CViewModel result = await handler.Get(dto);

        return StatusCode(handler.StatusCode, result);
    }
}
