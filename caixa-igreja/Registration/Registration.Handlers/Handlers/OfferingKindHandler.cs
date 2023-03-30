﻿using AutoMapper;
using Scode = HttpCodeLib.NumberStatusCode;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Registration.DomainCore.HandlerAbstraction;
using Registration.DomainCore.ContextAbstraction;
using Registration.Handlers.Queries;
using Registration.DomainBase.Entities;
using Registration.Mapper.DTOs.OfferingKind;
using Registration.DomainCore.ViewModelAbstraction;

namespace ChurchCashFlow.Handlers;
public class OfferingKindHandler : IHandler<ReadOfferingKindDto, EditOfferingKindDto>
{
    private IOfferingKindRepository _context;
    private IMapper _mapper;
    private int _statusCode;
    private readonly CViewModel _viewModel;

    public OfferingKindHandler(IOfferingKindRepository context, IMapper mapper, CViewModel viewModel)
    {
        _context = context;
        _mapper = mapper;
        _viewModel = viewModel;
    }

    public int GetStatusCode() => (int)_statusCode;

    public async Task<CViewModel> GetAll(bool active = true)
    {
        try
        {
            var offeringKindExpression  = Querie<OfferingKind>.GetActive(active);

            var offeringKindQuery = _context.GetAll();
            var offeringKind = await offeringKindQuery.Where(offeringKindExpression).ToListAsync();

            var offeringKindReadDto = _mapper.Map<IEnumerable<ReadOfferingKindDto>>(offeringKind);

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringKindReadDto);
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel.SetErrors("Internal Error - OF101A");
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetOne(int id)
    {
        try
        {
            var OfferingKind = await _context.GetOneAsNoTracking(id);
            if (OfferingKind == null)
            {
                _statusCode = (int)Scode.NOT_FOUND;
                _viewModel.SetErrors("Object not found");

                return _viewModel;
            }

            _statusCode = (int)Scode.OK;

            var OfferingKindReadDto = _mapper.Map<ReadOfferingKindDto>(OfferingKind);
            _viewModel.SetData(OfferingKindReadDto);
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel.SetErrors("Internal Error - OF102A");
        }

        return _viewModel;
    }

    public async Task<CViewModel> Create(EditOfferingKindDto OfferingKindEditDto)
    {
        OfferingKindEditDto.Validate();
        if (!OfferingKindEditDto.IsValid)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel.SetErrors(OfferingKindEditDto.GetNotification());

            return _viewModel;
        }

        try
        {
            var OfferingKind = _mapper.Map<OfferingKind>(OfferingKindEditDto);

            await _context.Post(OfferingKind)!;

            var newOffering = await _context.GetOne(OfferingKind.Id);

            ReadOfferingKindDto offeringReadDto = _mapper.Map<ReadOfferingKindDto>(newOffering);
            _statusCode = (int)Scode.CREATED;

            _viewModel.SetData(offeringReadDto);
        }
        catch (DbUpdateException)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel.SetErrors("Request Error. Check the properties - OF103A");
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel.SetErrors("Internal Error. - OF103B");
        }

        return _viewModel;
    }

    public async Task<CViewModel> Delete(int id)
    {
        try
        {
            var OfferingKind = await _context.GetOne(id);
            if (OfferingKind == null)
            {
                _statusCode = (int)Scode.NOT_FOUND;
                _viewModel.SetErrors("Object not found");

                return _viewModel;
            }

            await _context.Delete(OfferingKind);

            _statusCode = (int)Scode.OK;
        }
        catch (DbException ex)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel.SetErrors("Request Error. Check the properties - OF104A");
        }
        catch
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel.SetErrors("Internal Error - OF104B");
        }

        return _viewModel;
    }

    public Task<CViewModel> Update(EditOfferingKindDto churchEditDto, int id)
    {
        //ainda não implementado
        throw new NotImplementedException();
    }
}