using System.Data.Common;
using Microsoft.Data.Sqlite;
using MyRestAPI.Common.DB;
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

    private readonly DbConnectionWrapper<SqliteConnection> _dbWrapper;

    public string TableName => "Client";

    public Client_Table(DbConnectionWrapper<SqliteConnection> dbWrapper)
    {
        _dbWrapper = dbWrapper ?? throw new ArgumentNullException(nameof(dbWrapper));
    }

    public bool Exists(DbConnection connection, DbTransaction? transaction = null)
    {
        using var command = connection.CreateCommand();
        if (transaction != null)
        {
            command.Transaction = transaction;
        }
        command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = @name;";
        var parameter = command.CreateParameter();
        parameter.ParameterName = "@name";
        parameter.Value = TableName;
        command.Parameters.Add(parameter);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public void EnsureCreated(DbConnection connection, DbTransaction? transaction = null)
    {
        if (Exists(connection, transaction))
        {
            return;
        }

        using var command = connection.CreateCommand();
        if (transaction != null)
        {
            command.Transaction = transaction;
        }
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
