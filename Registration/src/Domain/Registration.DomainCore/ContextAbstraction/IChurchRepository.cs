﻿using Registration.DomainBase.Entities.Registrations;

namespace Registration.DomainCore.ContextAbstraction;
public interface IChurchRepository
{
    public IQueryable<Church>? GetAll(bool active);
    public Task<Church> GetOne(int id);
    public Task<Church> GetOneNoTracking(int id);
    public Task Post(Church church);
    public Task Put(Church church);
    public Task Delete(Church church);
}
