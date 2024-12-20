﻿using AutoMapper;
using CloudServices.AWS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Registration.DomainBase.Entities.Registrations;
using Registration.DomainCore.CloudAbstration;
using Registration.DomainCore.ContextAbstraction;
using Registration.DomainCore.HandlerAbstraction;
using Registration.DomainCore.ViewModelAbstraction;
using Registration.Handlers.CloudHandlers;
using Registration.Handlers.Queries;
using Registration.Mapper.DTOs.Registration.FirstFruits;
using Registration.Mapper.DTOs.Registration.Offering;
using Serilog;
using System.Data.Common;
using Scode = HttpCodeLib.NumberStatusCode;

namespace Registration.Handlers.Handlers.Registrations;
public sealed class OfferingHandler : BaseRegisterNormalHandler
{
    private IOfferingRepository _context;
    private OperationsHandler _operationsHandler;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IImageStorage _storage;
    private readonly IMemoryCache _cache;
    private const string _cacheKey = "OFFERINGS";

    private static Dictionary<string, IEnumerable<ReadOfferingDto>?> HashGetByPeriod = new();
    public OfferingHandler(IOfferingRepository context, IMapper mapper, CViewModel viewModel, OperationsHandler operationsHandler, ILogger logger, IConfiguration configuration, IImageStorage storage, IMemoryCache cache)
        : base(mapper, viewModel)
    {
        _context = context;
        _operationsHandler = operationsHandler;
        _logger = logger;
        _configuration = configuration;
        _storage = storage;
        _cache = cache;
    }

    protected override async Task<bool> MonthWorkIsBlockAsync(string competence, int churchId)
    {
        var yearMonth = DateTime.Parse(competence).ToString("yyyyMM");
        var monthWork = await _operationsHandler.GetOneByCompetence(yearMonth, churchId);

        if(monthWork is not null)
            return monthWork.Active == false ? false : true;

        return false;
    }

    public async Task<CViewModel> GetAllAsync(int churchId, bool active = true)
    {
        IEnumerable<ReadOfferingDto>? offeringReadDto;

        try
        {
            offeringReadDto = await _cache.GetOrCreateAsync(_cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeToExpirationCache;

                var offeringExpression = Querie<Offering>.GetActive(active);

                var offeringQuery = _context.GetAll(churchId);
                var offering = await offeringQuery
                    .Where(offeringExpression)
                    .Include(x => x.MeetingKind)
                    .Include(x => x.OfferingKind)
                    .Include(x => x.Church)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ReadOfferingDto>>(offering);
            });

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);
        }
        catch(Exception ex)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF01A");
            _logger.Error("Fail get all OF01A {erro}", ex.Message);
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetAllLimitAsync(int churchId, bool active, int limit)
    {
        IEnumerable<ReadOfferingDto>? offeringReadDto;

        try
        {
            offeringReadDto = await _cache.GetOrCreateAsync(_cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeToExpirationCache;

                var offeringExpression = Querie<Offering>.GetActive(active);

                var offeringQuery = _context.GetAll(churchId);
                var offering = await offeringQuery
                    .Where(offeringExpression)
                    .Include(x => x.MeetingKind)
                    .Include(x => x.OfferingKind)
                    .Include(x => x.Church)
                    .Take(limit)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ReadOfferingDto>>(offering);
            });

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);
        }
        catch(Exception ex)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF02A");
            _logger.Error("Fail - get all with limit {error} - OF92A", ex.Message);
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetByPeriodAsync(int churchId, string initialDate, string finalDate, bool active)
    {
        IEnumerable<ReadOfferingDto>? offeringReadDto;

        try
        {
            if (!ValidateCompetence(initialDate) | !ValidateCompetence(finalDate))
            {
                _statusCode = (int)Scode.BAD_REQUEST;
                _viewModel!.SetErrors("Request Error. Check the properties - OF01B");
                _logger.Error("The competence {initialDate}~{finalDate} is invalid", initialDate, finalDate);

                return _viewModel;
            }

            initialDate = DateTime.Parse(initialDate).ToString("yyyy-MM-dd");
            finalDate = DateTime.Parse(finalDate).ToString("yyyy-MM-dd");

            var cacheName = $"{_cacheKey}-{churchId}-{initialDate}-{finalDate}";
            offeringReadDto = await _cache.GetOrCreateAsync(cacheName, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeToExpirationCache;

                var offeringExpression = Querie<Offering>.GetActive(active);

                var offeringQuery = _context.GetAll(churchId);
                var offering = await offeringQuery
                    .Where(offeringExpression)
                    .Where(x => x.Day >= DateTime.Parse(initialDate))
                    .Where(x => x.Day <= DateTime.Parse(finalDate))
                    .Include(x => x.MeetingKind)
                    .Include(x => x.OfferingKind)
                    .Include(x => x.Church)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ReadOfferingDto>>(offering);
            });

            HashGetByPeriod.TryAdd(cacheName, offeringReadDto);

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);
        }
        catch(Exception ex)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF02B");
            _logger.Error("Failt get by period", ex.Message);
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetAllByCompetenceAsync(int churchId, string yearMonth, bool active = true)
    {
        var competence = $"{yearMonth.Substring(0, 4)}-{yearMonth.Substring(4, 2)}-01";

        var initialDate = $"01-{DateTime.Parse(competence).ToString("MM")}-{DateTime.Parse(competence).ToString("yyyy")}";
        var lastday = DateTime.DaysInMonth(int.Parse(DateTime.Parse(competence).ToString("yyyy")), 
                        int.Parse(DateTime.Parse(competence).ToString("MM")) );
        var finalDate = $"{lastday}-{DateTime.Parse(competence).ToString("MM")}-{DateTime.Parse(competence).ToString("yyyy")}";

        return await GetByPeriodAsync(churchId, initialDate, finalDate, active);
    }

    public async Task<CViewModel> GetOneAsync(int id)
    {
        ReadOfferingDto? offeringReadDto;
        Offering? offering;

        try
        {
            offeringReadDto = await _cache.GetOrCreateAsync($"{_cacheKey}-{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeToExpirationCache;

                offering = TryGetOne(id);
                return _mapper.Map<ReadOfferingDto>(offering);
            });

            if (offeringReadDto == null)
                return _viewModel;

            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);
        }
        catch(Exception ex)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF01C");
            _logger.Error("Fail - get one {error} - OF01C", ex.Message);
        }

        return _viewModel;
    }

    public async Task<CViewModel> GetOneByChurchAsync(int churchId, int id)
    {
        ReadOfferingDto? offeringReadDto;
        Offering? offering;
        try
        {
            offeringReadDto = await _cache.GetOrCreateAsync($"{_cacheKey}-{churchId}-{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeToExpirationCache;

                offering = TryGetOneByChurch(churchId, id);
                return _mapper.Map<ReadOfferingDto>(offering);
            });
            
            if (offeringReadDto == null)
                return _viewModel;
            
            _statusCode = (int)Scode.OK;
            _viewModel.SetData(offeringReadDto);

            _logger.Information("The offering with id {id} was found", id);
        }
        catch(Exception ex)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF02C");
            _logger.Error("Fail - get one by church {error} - OF02C", ex.Message);
        }

        return _viewModel;
    }

    public async Task<CViewModel> CreateAsync(EditOfferingDto dto)
    {
        dto.Validate();
        if (!dto.IsValid)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel!.SetErrors(dto.GetNotification());
            _logger.Error("Invalid properties. Check the properties");
            return _viewModel;
        }

        if (await MonthWorkIsBlockAsync(dto.Day.ToString(), dto.ChurchId))
        {
            _statusCode = (int)Scode.NOT_ACCEPTABLE;
            _viewModel!.SetErrors("This competence has already been closed!");
            _logger.Error("The competence has already been closed");
            return _viewModel;
        }

        try
        {
            var offering = _mapper.Map<Offering>(dto);

            offering.UpdateData();
            await _context.Post(offering);

            await SaveImageStoreAsync(offering.Photo!, dto.base64Image);

            var newOffering = await _context.GetOneAsNoTracking(offering.Id);

            var offeringReadDto = _mapper.Map<ReadOfferingDto>(newOffering);

            _cache.Remove(_cacheKey);
            foreach (var item in HashGetByPeriod)
                _cache.Remove(item.Key);

            _statusCode = (int)Scode.CREATED;

            _viewModel.SetData(offeringReadDto);
        }
        catch (DbUpdateException ex)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel!.SetErrors("Request Error. Check the properties - OF01D");
            _logger.Error("Fail - create one {error} - OF01D", ex.Message);
        }
        catch (Exception ex)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error. - OF02D");
            _logger.Error("Fail - create one {error} - OF02D", ex.Message);
        }

        return _viewModel;
    }

    public async Task<CViewModel> UpdateAsync(EditOfferingDto dto, int id)
    {
        _logger.Information("Offering - attemp update one");

        dto.Validate();
        if (!dto.IsValid)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel!.SetErrors(dto.GetNotification());
            _logger.Error("Invalid properties. Check the properties");
            return _viewModel;
        }

        if (await MonthWorkIsBlockAsync(dto.Day.ToString(), dto.ChurchId))
        {
            _statusCode = (int)Scode.NOT_ACCEPTABLE;
            _viewModel!.SetErrors("This competence has already been closed!");
            _logger.Error("The competence has already been closed");
            return _viewModel;
        }

        try
        {
            var offering = TryGetOne(id);
            if (offering == null)
                return _viewModel;

            var editOffering = _mapper.Map<Offering>(dto);
            offering.UpdateChanges(editOffering);

            offering.UpdateData();
            await SaveImageStoreAsync(offering.Photo!, dto.base64Image);

            await _context.Put(offering);

            _cache.Remove(_cacheKey);
            _cache.Remove($"{_cacheKey}{id}");
            _cache.Remove($"{_cacheKey}-{dto.ChurchId}-{id}");
            foreach(var item in HashGetByPeriod)
                _cache.Remove(item.Key);

            _statusCode = (int)Scode.OK;

            _logger.Information("The offering was successfully updated");
            _viewModel.SetData("The offering was successfully updated");
        }
        catch (DbUpdateException ex)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel!.SetErrors("Request Error. Check the properties - OF01E");
            _logger.Error("Fail - update one {error} - OF01E", ex.Message);
        }
        catch(Exception ex)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error. - OF02E");
            _logger.Error("Fail - update one {error} - OF02E", ex.Message);
        }

        return _viewModel;
    }

    public async Task<CViewModel> DeleteAsync(int id)
    {
        _logger.Information("Offering - attemp delete one");

        try
        {
            var offering = TryGetOne(id);
            if (offering == null)
                return _viewModel;

            if (await MonthWorkIsBlockAsync(offering.Day.ToString(), offering.ChurchId))
            {
                _statusCode = (int)Scode.NOT_ACCEPTABLE;
                _viewModel!.SetErrors("This competence has already been closed!");
                _logger.Error("Invalid properties. Check the properties");
                return _viewModel;
            }

            await _context.Delete(offering);

            _statusCode = (int)Scode.OK;

            _cache.Remove(_cacheKey);
            _cache.Remove($"{_cacheKey}{id}");
            _cache.Remove($"{_cacheKey}-{offering.ChurchId}-{id}");
            foreach (var item in HashGetByPeriod)
                _cache.Remove(item.Key);

            _logger.Information("The offering was successfully delete");
        }
        catch (DbException ex)
        {
            _statusCode = (int)Scode.BAD_REQUEST;
            _viewModel.SetData("Request Error. Check the properties - OF01F");
            _logger.Error("Fail - delete one {error} - OF01F", ex.Message);
        }
        catch(Exception ex)
        {
            _statusCode = (int)Scode.INTERNAL_SERVER_ERROR;
            _viewModel!.SetErrors("Internal Error - OF01F");
            _logger.Error("Fail - delete one {error} - OF01F", ex.Message);
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
            _logger.Information("Offering with id {id} was not found", id);
            return null;
        }

        return offering.Result;
    }

    private async Task SaveImageStoreAsync(string fileName, string? base64Image)
    {
        ModelImage serviceImage = new("offerings", fileName, _logger, _storage);
        await serviceImage.SaveImageStoreAsync(base64Image);
    }

    private Offering? TryGetOneByChurch(int churchId, int id)
    {
        var offering = _context.GetOneByChurch(churchId, id);
        if (offering.Result == null)
        {
            _statusCode = (int)Scode.OK;
            _viewModel!.SetErrors("Object not found");
            _logger.Information("Offering with id {id} was not found for church {idChurch}", id, churchId);
            return null;
        }

        return offering.Result;
    }
}
