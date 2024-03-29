﻿using Microsoft.AspNetCore.Mvc;
using Registration.API.Extensions;
using Registration.DomainCore.ViewModelAbstraction;
using Microsoft.AspNetCore.Authorization;
using Registration.Handlers.Handlers.Registrations;
using Registration.Mapper.DTOs.Registration.User;
using Registration.Mapper.DTOs.Registration.UserLogin;

namespace Registration.API.Controllers.Registrations;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserHandler _handler;
    private readonly CViewModel? _viewModel;

    private string rolles = "";

    public UserController(UserHandler handler, CViewModel viewModel)
    {
        _handler = handler;
        _viewModel = viewModel;
    }

    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/user")]
    public async Task<IActionResult> GetAll([FromQuery] bool active = true)
    {
        var resultViewModel = await _handler.GetAll(active);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/user/{id:int}")]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        var resultViewModel = await _handler.GetOne(id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT")]
    [HttpPost("api/v1/user")]
    public async Task<IActionResult> Create([FromBody] EditUserCreateDto userEditDto)
    {
        if (!ModelState.IsValid)
        {
            _viewModel!.SetErrors(ModelState.GetErrors());
            return BadRequest(_viewModel);
        }

        var resultViewModel = await _handler.Create(userEditDto);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT")]
    [HttpPut("api/v1/user/{id:int}")]
    public async Task<IActionResult> Update([FromBody] EditUserDto userEditDto, int id)
    {
        if (!ModelState.IsValid)
        {
            _viewModel!.SetErrors(ModelState.GetErrors());
            return BadRequest(_viewModel);
        }

        var resultViewModel = await _handler.Update(userEditDto, id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT")]
    [HttpDelete("/api/v1/user/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultViewModel = await _handler.Delete(id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }
}