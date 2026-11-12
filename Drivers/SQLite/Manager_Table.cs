using System.Data.Common;
using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite;

public class Manager_Table : IManager, ITableInitializer
{
    private const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Manager (
            Manager_id INTEGER PRIMARY KEY AUTOINCREMENT,
            Full_Manager_Name TEXT NOT NULL,
            Post TEXT NOT NULL,
            Phone_number TEXT NOT NULL,
            Email TEXT NOT NULL
        );
        """;

    private readonly string _connectionString;

    public string TableName => "Manager";

    public Manager_Table(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool Exists(DbConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = @name;";
        var parameter = command.CreateParameter();
        parameter.ParameterName = "@name";
        parameter.Value = TableName;
        command.Parameters.Add(parameter);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public void EnsureCreated(DbConnection connection)
    {
        if (Exists(connection))
        {
            return;
        }

        using var command = connection.CreateCommand();
        command.CommandText = CreateSql;
        command.ExecuteNonQuery();
    }

    public List<Manager> Get() => throw new NotImplementedException();
    public Manager? Get(int id) => throw new NotImplementedException();
    public void Post(Manager item) => throw new NotImplementedException();
    public void Put(int id, Manager item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
    public Manager? GetByEmail(string email) => throw new NotImplementedException();
    public List<Manager> GetByPost(string post) => throw new NotImplementedException();
}
