﻿using Registration.DomainBase.Entities.Registrations;

namespace Registration.DomainCore.ContextAbstraction;

public interface IMonthWorkRepository
{
    public Task Create(MonthWork monthWork);
    public Task Remove(MonthWork monthWork);
    public Task<MonthWork?> GetOne(int id);
    public Task<MonthWork?> GetOneAsNoTracking(int id);
    public IQueryable<MonthWork> GetAll(int churchId);
    public Task<MonthWork?> GetOneByCompetenceAsNoTracking(int yearMonth, int churchId);
}
