using System.Data.Common;

namespace MyRestAPI.Drivers.MariaDB;

internal interface ITableInitializer
{
    string TableName { get; }
    bool Exists(DbConnection connection);
    void EnsureCreated(DbConnection connection);
}

