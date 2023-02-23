﻿using AutoMapper;
using ChurchCashFlow.Data;
using ChurchCashFlow.Data.ViewModels.Dtos.User;
using ChurchCashFlow.Extensions;
using ChurchCashFlow.Data.Entities;
using ChurchCashFlow.Data.ViewModels;
using ChurchCashFlow.Data.ViewModels.Dtos.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using System.Data.Common;

namespace ChurchCashFlow.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserController(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("api/v1/user")]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive)
    {
        IEnumerable<User> users;

        try
        {
            if (onlyActive)
                users = await _context.Users.Include(x => x.Church).Include(x => x.Role).AsNoTracking().Where(x => x.Active == true).ToListAsync();
            else
                users = await _context.Users.Include(x => x.Church).Include(x => x.Role).AsNoTracking().ToListAsync();

            IEnumerable<ReadUserDto> usersReadDto = _mapper.Map<IEnumerable<ReadUserDto>>(users);

            return Ok(new ResultViewModel<IEnumerable<ReadUserDto>>(usersReadDto));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Error - US1101A"));
        } 
    }

    [HttpGet("api/v1/user/{id:int}")]
    public async Task<IActionResult> GetAll([FromRoute] int id)
    {
        try
        {
            var user = await _context.Users.Include(x => x.Church).Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound(new ResultViewModel<dynamic>("Object not found", null));

            ReadUserDto userReadDto = _mapper.Map<ReadUserDto>(user);

            return Ok(new ResultViewModel<ReadUserDto>(userReadDto));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Error - US1102A"));
        }
    }

    [HttpPost("api/v1/user")]
    public async Task<IActionResult> Post([FromBody] EditUserDto userEditDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            User user = _mapper.Map<User>(userEditDto);
            user.PassWordHash = PasswordHasher.Hash(userEditDto.PassWordHash);

            var code = Guid.NewGuid().ToString().ToUpper();
            code = code.Substring(0, 6);

            user.Code = code;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            user = await 
                _context.Users.Include(x => x.Church).Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.Id);

            ReadUserDto userReadDto = _mapper.Map<ReadUserDto>(user);

            return Created($"/api/v1/user/{user.Id}", new ResultViewModel<ReadUserDto>(userReadDto));
        }
        catch(DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("Internal Error. Check the properties - US1103A"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Error. - US1103B"));
        }
    }

    [HttpPut("api/v1/user/{id:int}")]
    public async Task<IActionResult> Put([FromBody] EditUserDto userEditDto, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            var user = await _context.Users.Include(x => x.Church).Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound(new ResultViewModel<dynamic>("Object not found", null));

            user = _mapper.Map(userEditDto, user);

            await _context.SaveChangesAsync();

            ReadUserDto userReadDto = _mapper.Map<ReadUserDto>(user);

            return Ok(new ResultViewModel<ReadUserDto>(userReadDto));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("Internal Error. Check the properties - US1104A"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Error. - US1104B"));
        }
    }

    [HttpDelete]
    [Route("/api/v1/user/{id:int}")]
    public async Task<IActionResult> DeleteChurch(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            return NotFound(new ResultViewModel<dynamic>("Object not found", null));

        try
        {
            user.Active = false;
            ReadUserDto userReadDto = _mapper.Map<ReadUserDto>(user);

            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<ReadUserDto>(userReadDto));
        }
        catch (DbException)
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Error. Check the properties - US1104A"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Internal Error - US1104B"));
        }
    }

}