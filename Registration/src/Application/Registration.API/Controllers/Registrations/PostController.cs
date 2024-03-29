﻿using Microsoft.AspNetCore.Mvc;
using Registration.API.Extensions;
using Registration.DomainCore.ViewModelAbstraction;
using Microsoft.AspNetCore.Authorization;
using Registration.Handlers.Handlers.Registrations;
using Registration.Mapper.DTOs.Registration.Post;

namespace Registration.API.Controllers.Registrations;

[ApiController]
public class PostController : ControllerBase
{
    private readonly PostHandler _handler;
    private readonly CViewModel? _viewModel;

    public PostController(PostHandler handler)
    {
        _handler = handler;
    }

    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/post")]
    public async Task<IActionResult> GetAll([FromQuery] bool active = true)
    {
        var resultViewModel = await _handler.GetAll(active);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT, M-TRS, L-TRS")]
    [HttpGet("api/v1/post/{id:int}")]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        var resultViewModel = await _handler.GetOne(id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT")]
    [HttpPost("api/v1/post")]
    public async Task<IActionResult> Create([FromBody] EditPostDto postEditDto)
    {
        if (!ModelState.IsValid)
        {
            _viewModel!.SetErrors(ModelState.GetErrors());
            return BadRequest(_viewModel);
        }

        var resultViewModel = await _handler.Create(postEditDto);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT")]
    [HttpPut("api/v1/post/{id:int}")]
    public async Task<IActionResult> Update([FromBody] EditPostDto postEditDto, int id)
    {
        if (!ModelState.IsValid)
        {
            _viewModel!.SetErrors(ModelState.GetErrors());
            return BadRequest(_viewModel);
        }

        var resultViewModel = await _handler.Update(postEditDto, id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }

    [Authorize(Roles = "L-SCT, M-SCT")]
    [HttpDelete("/api/v1/post/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resultViewModel = await _handler.Delete(id);

        return StatusCode(_handler.GetStatusCode(), resultViewModel);
    }
}