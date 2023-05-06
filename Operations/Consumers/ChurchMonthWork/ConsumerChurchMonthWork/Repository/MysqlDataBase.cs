﻿using ConsumerChurchMonthWork.Entitie;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ConsumerChurchMonthWork.Repository;

public class MysqlDataBase : IDataBase, IDisposable
{
    private readonly IConfiguration _configuration;
    private MySqlConnection _mysqlConnection;
    public MysqlDataBase(IConfiguration configuration)
    {
        _configuration = configuration;

        var conStr = _configuration.GetConnectionString("DataConnection");

        _mysqlConnection = new MySqlConnection(conStr);
    }

    public IEnumerable<MonthlyClosing> SelectReport()
    {
        var query = "SELECT * FROM OutFlow";

        var objMonthlyClosing = _mysqlConnection.Query(query);

        
        return null;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}