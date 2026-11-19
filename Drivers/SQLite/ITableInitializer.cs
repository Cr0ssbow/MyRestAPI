using System.Data.Common;

namespace MyRestAPI.Drivers.SQLite;

internal interface ITableInitializer
{
    string TableName { get; }
    bool Exists(DbConnection connection, DbTransaction? transaction = null);
    void EnsureCreated(DbConnection connection, DbTransaction? transaction = null);
}

