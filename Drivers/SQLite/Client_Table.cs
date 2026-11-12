using System.Data.Common;
using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite;

public class Client_Table : IClient, ITableInitializer
{
    private const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Client (
            ClientId INTEGER PRIMARY KEY AUTOINCREMENT,
            FullName TEXT NOT NULL,
            PassportSeries TEXT NOT NULL,
            PassportNumber TEXT NOT NULL,
            BirthDate TEXT NOT NULL,
            PhoneNumber TEXT NOT NULL,
            Address TEXT NOT NULL,
            Email TEXT
        );
        """;

    private readonly string _connectionString;

    public string TableName => "Client";

    public Client_Table(string connectionString)
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

    public List<Client> Get() => throw new NotImplementedException();
    public Client? Get(int id) => throw new NotImplementedException();
    public void Post(Client item) => throw new NotImplementedException();
    public void Put(int id, Client item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
    public Client? GetByPassport(string series, string number) => throw new NotImplementedException();
    public Client? GetByPhone(string phoneNumber) => throw new NotImplementedException();
    public List<Client> SearchByName(string namePart) => throw new NotImplementedException();
}
