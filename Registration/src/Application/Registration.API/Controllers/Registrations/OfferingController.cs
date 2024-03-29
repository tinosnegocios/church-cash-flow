﻿using Microsoft.AspNetCore.Mvc;
using Registration.API.Extensions;
using Registration.DomainCore.ViewModelAbstraction;
using Microsoft.AspNetCore.Authorization;
using Registration.Handlers.Handlers.Registrations;
using Registration.Mapper.DTOs.Registration.Offering;

namespace Registration.API.Controllers.Registrations;
public class OfferingController : ControllerBase
{
    private readonly OfferingHandler _handler;
    private readonly CViewModel? _viewModel;

    public OfferingController(OfferingHandler handler, CViewModel? viewModel)
    {
        _handler = handler;
        _viewModel = viewModel;
    }

    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/offering/all/{churchId:int}")]
    public async Task<IActionResult> GetAll([FromRoute] int churchId,
        [FromQuery] bool active = true)
    {
        var resultViewModel = await _handler.GetAllAsync(churchId, active);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/offering/limit/{churchId:int}/{limit:int}")]
    public async Task<IActionResult> GetAllLimit([FromRoute] int churchId, [FromRoute] int limit,
    [FromQuery] bool active = true)
    {
        var resultViewModel = await _handler.GetAllLimitAsync(churchId, active, limit);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/offering/period/{churchId:int}/")]
    public async Task<IActionResult> GetByPeriod([FromRoute] int churchId, [FromQuery] string initialDate, [FromQuery] string finalDate,
    [FromQuery] bool active = true)
    {
        var resultViewModel = await _handler.GetByPeriodAsync(churchId,initialDate, finalDate, active);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/offering/all/{churchId:int}/{yearMonth:int}")]
    public async Task<IActionResult> GetAllByCompetence([FromRoute] int churchId,
    [FromRoute] int yearMonth,
    [FromQuery] bool active = true)
    {
        var resultViewModel = await _handler.GetAllByCompetenceAsync(churchId, yearMonth.ToString(), active);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "M-TRS, L-TRS")]
    [HttpGet("api/v1/offering/{id:int}")]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        var resultViewModel = await _handler.GetOneAsync(id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "M-TRS, L-TRS")]
    [HttpGet("api/v1/offering/{churchId:int}/{id:int}")]
    public async Task<IActionResult> GetOneByChurch([FromRoute] int churchId, [FromRoute] int id)
    {
        var resultViewModel = await _handler.GetOneByChurchAsync(churchId, id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "M-TRS, L-TRS")]
    [HttpPost("api/v1/offering")]
    public async Task<IActionResult> Create([FromBody] EditOfferingDto offeringEditDto)
    {
        if (!ModelState.IsValid)
        {
            _viewModel!.SetErrors(ModelState.GetErrors());
            return BadRequest(_viewModel);
        }

        var resultViewModel = await _handler.CreateAsync(offeringEditDto);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "M-TRS, L-TRS")]
    [HttpPut("api/v1/offering/{id:int}")]
    public async Task<IActionResult> Update([FromBody] EditOfferingDto offeringEditDto, int id)
    {
        if (!ModelState.IsValid)
        {
            _viewModel!.SetErrors(ModelState.GetErrors());
            return BadRequest(_viewModel);
        }

        var resultViewModel = await _handler.UpdateAsync(offeringEditDto, id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "M-TRS, L-TRS")]
    [HttpDelete("/api/v1/offering/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultViewModel = await _handler.DeleteAsync(id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }
}
