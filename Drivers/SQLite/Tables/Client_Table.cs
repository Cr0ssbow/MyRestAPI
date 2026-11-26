using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite.Tables;

public class Client_Table : IClient
{
    internal const string CreateSql = """
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

    private readonly SqliteExecutor<Client> _executor;

    public Client_Table(SqliteExecutor<Client> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
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

