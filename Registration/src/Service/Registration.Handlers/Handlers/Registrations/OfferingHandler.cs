﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Registration.DomainBase.Entities.Registrations;
using Registration.DomainCore.ContextAbstraction;
using Registration.DomainCore.HandlerAbstraction;
using Registration.DomainCore.ViewModelAbstraction;
using Registration.Handlers.Queries;
using Registration.Mapper.DTOs.Registration.Offering;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Scode = HttpCodeLib.NumberStatusCode;

namespace Registration.Handlers.Handlers.Registrations;
public sealed class OfferingHandler : BaseRegisterNormalHandler
{
    private IOfferingRepository _context;
    private OperationsHandler _operationsHandler;

    public OfferingHandler(IOfferingRepository context, IMapper mapper, CViewModel viewModel, OperationsHandler operationsHandler) : base(mapper, viewModel)
    {
        _context = context;
        _operationsHandler = operationsHandler;
    }

    protected override async Task<bool> MonthWorkIsBlockAsync(string competence, int churchId)
    {
        var yearMonth = DateTime.Parse(competence).ToString("yyyyMM");
        var monthWork = await _operationsHandler.GetOneByCompetence(yearMonth, churchId);

        return monthWork == null ? false : true;
    }

    public async Task<CViewModel> GetAllAsync(int churchId, bool active = true)
    {
        try
        {
            var offeringExpression = Querie<Offering>.GetActive(active);

            var offeringQuery = _context.GetAll(churchId);
            var offering = await offeringQuery
                .Where(offeringExpression)
                .Include(x => x.MeetingKind)
                .Include(x => x.OfferingKind)
                .Include(x => x.Church)
                .ToListAsync();

            var offeringReadDto = _mapper.Map<IEnumerable<ReadOfferingDto>>(offering);

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF01A");
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetAllLimitAsync(int churchId, bool active, int limit)
    {
        try
        {
            var offeringExpression = Querie<Offering>.GetActive(active);

            var offeringQuery = _context.GetAll(churchId);
            var offering = await offeringQuery
                .Where(offeringExpression)
                .Include(x => x.MeetingKind)
                .Include(x => x.OfferingKind)
                .Include(x => x.Church)
                .Take(limit)
                .ToListAsync();

            var offeringReadDto = _mapper.Map<IEnumerable<ReadOfferingDto>>(offering);

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF02A");
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetByPeriodAsync(int churchId, string initialDate, string finalDate, bool active)
    {
        try
        {
            if (!ValidateCompetence(initialDate) | !ValidateCompetence(finalDate))
            {
                _statusCode = (int)Scode.BAD_REQUEST;
                _viewModel!.SetErrors("Request Error. Check the properties - OF01B");

                return _viewModel;
            }

            var offeringExpression = Querie<Offering>.GetActive(active);

            initialDate = DateTime.Parse(initialDate).ToString("yyyy-MM-dd");
            finalDate = DateTime.Parse(finalDate).ToString("yyyy-MM-dd");

            var offeringQuery = _context.GetAll(churchId);
            var offering = await offeringQuery
                .Where(offeringExpression)
                .Where(x => x.Day >= DateTime.Parse(initialDate))
                .Where(x => x.Day <= DateTime.Parse(finalDate))
                .Include(x => x.MeetingKind)
                .Include(x => x.OfferingKind)
                .Include(x => x.Church)
                .ToListAsync();

            var offeringReadDto = _mapper.Map<IEnumerable<ReadOfferingDto>>(offering);

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF02B");
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetAllByCompetenceAsync(int churchId, string yearMonth, bool active = true)
    {
        try
        {
            var competence = $"{yearMonth.Substring(0, 4)}-{yearMonth.Substring(4, 2)}-01";

            if (!ValidateCompetence(competence))
            {
                _statusCode = (int)Scode.BAD_REQUEST;
                _viewModel!.SetErrors("Request Error. Check the properties - OF03B");

                return _viewModel;
            }

            var offeringExpression = Querie<Offering>.GetActive(active);

            var offeringQuery = _context.GetAll(churchId);
            var offering = await offeringQuery
                .Where(offeringExpression)
                .Where(x => x.Day.Year == DateTime.Parse(competence).Year && x.Day.Month == DateTime.Parse(competence).Month)
                .Include(x => x.MeetingKind)
                .Include(x => x.OfferingKind)
                .Include(x => x.Church)
                .ToListAsync();

            var offeringReadDto = _mapper.Map<IEnumerable<ReadOfferingDto>>(offering);

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);
        }
        catch (Exception)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF04B");
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetOneAsync(int id)
    {
        try
        {
            var offering = TryGetOne(id);
            if (offering == null)
                return _viewModel;

            _statusCode = (int)Scode.OK;

            var offeringReadDto = _mapper.Map<ReadOfferingDto>(offering);
            _viewModel.SetData(offeringReadDto);
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF01C");
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetOneByChurchAsync(int churchId, int id)
    {
        try
        {
            var offering = TryGetOneByChurch(churchId, id);
            if (offering == null)
                return _viewModel;

            _statusCode = (int)Scode.OK;

            var offeringReadDto = _mapper.Map<ReadOfferingDto>(offering);
            _viewModel.SetData(offeringReadDto);
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF02C");
        }

        return _viewModel;
    }

    public async Task<CViewModel> CreateAsync(EditOfferingDto offeringEditDto)
    {
        offeringEditDto.Validate();
        if (!offeringEditDto.IsValid)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel!.SetErrors(offeringEditDto.GetNotification());

            return _viewModel;
        }

        if (await MonthWorkIsBlockAsync(offeringEditDto.Day.ToString(), offeringEditDto.ChurchId))
        {
            _statusCode = (int)Scode.NOT_ACCEPTABLE;
            _viewModel!.SetErrors("This competence has already been closed!");

            return _viewModel;
        }

        try
        {
            var offering = _mapper.Map<Offering>(offeringEditDto);

            await _context.Post(offering);

            var newOffering = await _context.GetOneAsNoTracking(offering.Id);

            var offeringReadDto = _mapper.Map<ReadOfferingDto>(newOffering);
            _statusCode = (int)Scode.CREATED;

            _viewModel.SetData(offeringReadDto);
        }
        catch (DbUpdateException)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel!.SetErrors("Request Error. Check the properties - OF01D");
        }
        catch (Exception)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error. - OF02D");
        }

        return _viewModel;
    }

    public async Task<CViewModel> UpdateAsync(EditOfferingDto offeringEditDto, int id)
    {
        offeringEditDto.Validate();
        if (!offeringEditDto.IsValid)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel!.SetErrors(offeringEditDto.GetNotification());

            return _viewModel;
        }

        if (await MonthWorkIsBlockAsync(offeringEditDto.Day.ToString(), offeringEditDto.ChurchId))
        {
            _statusCode = (int)Scode.NOT_ACCEPTABLE;
            _viewModel!.SetErrors("This competence has already been closed!");

            return _viewModel;
        }

        try
        {
            var offering = TryGetOne(id);
            if (offering == null)
                return _viewModel;

            var editOffering = _mapper.Map<Offering>(offeringEditDto);
            offering.UpdateChanges(editOffering);

            await _context.Put(offering);

            _statusCode = (int)Scode.OK;
        }
        catch (DbUpdateException)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel!.SetErrors("Request Error. Check the properties - OF01E");
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error. - OF02E");
        }

        return _viewModel;
    }

    public async Task<CViewModel> DeleteAsync(int id)
    {
        try
        {
            var offering = TryGetOne(id);
            if (offering == null)
                return _viewModel;

            if (await MonthWorkIsBlockAsync(offering.Day.ToString(), offering.ChurchId))
            {
                _statusCode = (int)Scode.NOT_ACCEPTABLE;
                _viewModel!.SetErrors("This competence has already been closed!");

                return _viewModel;
            }

            await _context.Delete(offering);

            _statusCode = (int)Scode.OK;
        }
        catch (DbException)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel.SetData("Request Error. Check the properties - OF01F");
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF01F");
        }

        return _viewModel;
    }

    private Offering? TryGetOne(int id)
    {
        var offering = _context.GetOne(id);
        if (offering.Result == null)
        {
            _statusCode = (int)Scode.NOT_FOUND;
            _viewModel!.SetErrors("Object not found");

            return null;
        }

        return offering.Result;
    }


    private Offering? TryGetOneByChurch(int churchId, int id)
    {
        var offering = _context.GetOneByChurch(churchId, id);
        if (offering.Result == null)
        {
            _statusCode = (int)Scode.NOT_FOUND;
            _viewModel!.SetErrors("Object not found");

            return null;
        }

        return offering.Result;
    }
}
