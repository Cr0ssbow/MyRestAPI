using MyRestAPI.Common.DB.Entity;
using MyRestAPI.Common.DB.IDBUnits;

namespace MyRestAPI.Drivers.SQLite.Tables;

public class Manager_Table : IManager
{
    internal const string CreateSql = """
        CREATE TABLE IF NOT EXISTS Manager (
            Manager_id INTEGER PRIMARY KEY AUTOINCREMENT,
            Full_Manager_Name TEXT NOT NULL,
            Post TEXT NOT NULL,
            Phone_number TEXT NOT NULL,
            Email TEXT NOT NULL
        );
        """;

    private readonly SqliteExecutor<Manager> _executor;

    public Manager_Table(SqliteExecutor<Manager> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    public List<Manager> Get() => throw new NotImplementedException();
    public Manager? Get(int id) => throw new NotImplementedException();
    public void Post(Manager item) => throw new NotImplementedException();
    public void Put(int id, Manager item) => throw new NotImplementedException();
    public void Delete(int id) => throw new NotImplementedException();
    public Manager? GetByEmail(string email) => throw new NotImplementedException();
    public List<Manager> GetByPost(string post) => throw new NotImplementedException();
}

