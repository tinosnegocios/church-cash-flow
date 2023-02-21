﻿using ChurchCashFlow.Data.Mappings;
using ChurchCashFlow.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChurchCashFlow.Data;
public class DataContext : DbContext
{
    public DbSet<Church> Churches { get; set; }
    public DbSet<Address> Adresses { get; set; }

    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AddressMap());
        modelBuilder.ApplyConfiguration(new ChurchMap());
    }
}
